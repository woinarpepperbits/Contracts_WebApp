using ContractsApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractsApp.Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext für die Contracts-Anwendung
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Mandant> Mandants => Set<Mandant>();
    public DbSet<ContractGroup> ContractGroups => Set<ContractGroup>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<ContractPrice> ContractPrices => Set<ContractPrice>();
    public DbSet<PriceType> PriceTypes => Set<PriceType>();
    public DbSet<ContractCustomer> ContractCustomers => Set<ContractCustomer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Contract Configuration
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ContractNumber).IsUnique();
            entity.Property(e => e.ContractNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ResponsibleSales).HasMaxLength(100);
            entity.Property(e => e.ResponsibleAccounting).HasMaxLength(100);
            entity.Property(e => e.ResponsiblePricing).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(2000);

            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Contracts)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Mandant)
                .WithMany(m => m.Contracts)
                .HasForeignKey(e => e.MandantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ContractGroup)
                .WithMany(g => g.Contracts)
                .HasForeignKey(e => e.ContractGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Currency)
                .WithMany(c => c.Contracts)
                .HasForeignKey(e => e.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Customer Configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CustomerNumber).IsUnique();
            entity.Property(e => e.CustomerNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        // Mandant Configuration
        modelBuilder.Entity<Mandant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        // ContractGroup Configuration
        modelBuilder.Entity<ContractGroup>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        // Currency Configuration
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Code).HasMaxLength(3).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Symbol).HasMaxLength(5).IsRequired();
        });

        // ContractPrice Configuration
        modelBuilder.Entity<ContractPrice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,4)");
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(e => e.Contract)
                .WithMany(c => c.Prices)
                .HasForeignKey(e => e.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PriceType)
                .WithMany(pt => pt.ContractPrices)
                .HasForeignKey(e => e.PriceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // PriceType Configuration
        modelBuilder.Entity<PriceType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Code).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DefaultUnit).HasMaxLength(50);
        });

        // ContractCustomer Configuration
        modelBuilder.Entity<ContractCustomer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerNumber).HasMaxLength(50);
            entity.Property(e => e.AdvancePaymentAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.PaymentTerms).HasMaxLength(500);
            entity.Property(e => e.AccountingReference).HasMaxLength(100);

            entity.HasOne(e => e.Contract)
                .WithMany(c => c.ContractCustomers)
                .HasForeignKey(e => e.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed Data aufrufen
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Currencies
        var eurId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        modelBuilder.Entity<Currency>().HasData(
            new Currency
            {
                Id = eurId,
                Code = "EUR",
                Name = "Euro",
                Symbol = "€",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );

        // Seed Mandants
        var mandant1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        modelBuilder.Entity<Mandant>().HasData(
            new Mandant
            {
                Id = mandant1Id,
                Name = "Mandant 1",
                Code = "M1",
                Description = "Hauptmandant",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );

        // Seed ContractGroups
        var contractGroupId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        modelBuilder.Entity<ContractGroup>().HasData(
            new ContractGroup
            {
                Id = contractGroupId,
                Name = "Sonderkunden",
                Code = "SK",
                Description = "Vertrags-Sonderkunden EVU",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );

        // Seed PriceTypes
        var priceType1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var priceType2Id = Guid.Parse("44444444-4444-4444-4444-444444444445");
        modelBuilder.Entity<PriceType>().HasData(
            new PriceType
            {
                Id = priceType1Id,
                Name = "Arbeitspreis",
                Code = "AP",
                Description = "Arbeitspreis pro kWh",
                DefaultUnit = "kWh",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            },
            new PriceType
            {
                Id = priceType2Id,
                Name = "Grundpreis",
                Code = "GP",
                Description = "Monatlicher Grundpreis",
                DefaultUnit = "Monat",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );

        // Seed Customers
        var customer1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var customer2Id = Guid.Parse("55555555-5555-5555-5555-555555555556");
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = customer1Id,
                CustomerNumber = "K-12345",
                Name = "EVU Musterkunde GmbH",
                Address = "Musterstraße 123",
                PostalCode = "12345",
                City = "Musterstadt",
                Email = "kontakt@evu-musterkunde.de",
                Phone = "+49 123 456789",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            },
            new Customer
            {
                Id = customer2Id,
                CustomerNumber = "K-67890",
                Name = "Stadtwerke Beispielstadt AG",
                Address = "Energieweg 45",
                PostalCode = "67890",
                City = "Beispielstadt",
                Email = "info@stadtwerke-beispiel.de",
                Phone = "+49 987 654321",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );
    }
}
