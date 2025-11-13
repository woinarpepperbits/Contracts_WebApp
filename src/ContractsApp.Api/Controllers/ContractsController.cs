using ContractsApp.Api.DTOs;
using ContractsApp.Core.Models;
using ContractsApp.Core.Models.Enums;
using ContractsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractsApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContractsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContractsController> _logger;

    public ContractsController(ApplicationDbContext context, ILogger<ContractsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Hole alle Verträge
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractDto>>> GetContracts(
        [FromQuery] string? search = null,
        [FromQuery] ContractStatus? status = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25)
    {
        _logger.LogInformation("Getting contracts: search={Search}, status={Status}, page={Page}", search, status, page);

        var query = _context.Contracts
            .Include(c => c.Customer)
            .Include(c => c.Mandant)
            .Include(c => c.ContractGroup)
            .Include(c => c.Currency)
            .AsQueryable();

        // Filter nach Suche
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.ContractNumber.Contains(search) ||
                c.Customer!.Name.Contains(search) ||
                c.Customer.CustomerNumber.Contains(search));
        }

        // Filter nach Status
        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }

        // Pagination
        var totalCount = await query.CountAsync();
        var contracts = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dtos = contracts.Select(MapToDto).ToList();

        Response.Headers.Append("X-Total-Count", totalCount.ToString());
        Response.Headers.Append("X-Page", page.ToString());
        Response.Headers.Append("X-Page-Size", pageSize.ToString());

        return Ok(dtos);
    }

    /// <summary>
    /// Hole einen spezifischen Vertrag
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractDto>> GetContract(Guid id)
    {
        _logger.LogInformation("Getting contract {ContractId}", id);

        var contract = await _context.Contracts
            .Include(c => c.Customer)
            .Include(c => c.Mandant)
            .Include(c => c.ContractGroup)
            .Include(c => c.Currency)
            .Include(c => c.Prices)
            .Include(c => c.ContractCustomers)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contract == null)
        {
            return NotFound(new { message = $"Vertrag mit ID {id} nicht gefunden" });
        }

        return Ok(MapToDto(contract));
    }

    /// <summary>
    /// Erstelle einen neuen Vertrag
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractDto>> CreateContract(CreateContractDto dto)
    {
        _logger.LogInformation("Creating new contract {ContractNumber}", dto.ContractNumber);

        // Prüfe ob Vertragsnummer bereits existiert
        if (await _context.Contracts.AnyAsync(c => c.ContractNumber == dto.ContractNumber))
        {
            return BadRequest(new { message = $"Vertragsnummer {dto.ContractNumber} existiert bereits" });
        }

        // Validiere Referenzen
        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId))
        {
            return BadRequest(new { message = "Kunde nicht gefunden" });
        }

        if (!await _context.Mandants.AnyAsync(m => m.Id == dto.MandantId))
        {
            return BadRequest(new { message = "Mandant nicht gefunden" });
        }

        if (!await _context.ContractGroups.AnyAsync(g => g.Id == dto.ContractGroupId))
        {
            return BadRequest(new { message = "Vertragsgruppe nicht gefunden" });
        }

        if (!await _context.Currencies.AnyAsync(c => c.Id == dto.CurrencyId))
        {
            return BadRequest(new { message = "Währung nicht gefunden" });
        }

        var contract = new Contract
        {
            ContractNumber = dto.ContractNumber,
            CustomerId = dto.CustomerId,
            MandantId = dto.MandantId,
            ContractGroupId = dto.ContractGroupId,
            ContractType = dto.ContractType,
            Status = dto.Status,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsUnlimited = dto.IsUnlimited,
            NoticePeriodMonths = dto.NoticePeriodMonths,
            NoticeDeadline = dto.NoticeDeadline,
            AutoRenew = dto.AutoRenew,
            BillingStartDate = dto.BillingStartDate,
            ResponsibleSales = dto.ResponsibleSales,
            ResponsibleAccounting = dto.ResponsibleAccounting,
            ResponsiblePricing = dto.ResponsiblePricing,
            CurrencyId = dto.CurrencyId,
            Notes = dto.Notes,
            CreatedBy = "System", // TODO: Aus Authentication holen
            UpdatedBy = "System"
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Contract {ContractNumber} created with ID {ContractId}", contract.ContractNumber, contract.Id);

        // Lade vollständigen Contract für Response
        contract = await _context.Contracts
            .Include(c => c.Customer)
            .Include(c => c.Mandant)
            .Include(c => c.ContractGroup)
            .Include(c => c.Currency)
            .FirstAsync(c => c.Id == contract.Id);

        return CreatedAtAction(nameof(GetContract), new { id = contract.Id }, MapToDto(contract));
    }

    /// <summary>
    /// Aktualisiere einen Vertrag
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractDto>> UpdateContract(Guid id, UpdateContractDto dto)
    {
        _logger.LogInformation("Updating contract {ContractId}", id);

        var contract = await _context.Contracts
            .Include(c => c.Customer)
            .Include(c => c.Mandant)
            .Include(c => c.ContractGroup)
            .Include(c => c.Currency)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contract == null)
        {
            return NotFound(new { message = $"Vertrag mit ID {id} nicht gefunden" });
        }

        // Prüfe Vertragsnummer bei Änderung
        if (contract.ContractNumber != dto.ContractNumber)
        {
            if (await _context.Contracts.AnyAsync(c => c.ContractNumber == dto.ContractNumber && c.Id != id))
            {
                return BadRequest(new { message = $"Vertragsnummer {dto.ContractNumber} existiert bereits" });
            }
        }

        // Update properties
        contract.ContractNumber = dto.ContractNumber;
        contract.CustomerId = dto.CustomerId;
        contract.MandantId = dto.MandantId;
        contract.ContractGroupId = dto.ContractGroupId;
        contract.ContractType = dto.ContractType;
        contract.Status = dto.Status;
        contract.StartDate = dto.StartDate;
        contract.EndDate = dto.EndDate;
        contract.IsUnlimited = dto.IsUnlimited;
        contract.NoticePeriodMonths = dto.NoticePeriodMonths;
        contract.NoticeDeadline = dto.NoticeDeadline;
        contract.AutoRenew = dto.AutoRenew;
        contract.BillingStartDate = dto.BillingStartDate;
        contract.ResponsibleSales = dto.ResponsibleSales;
        contract.ResponsibleAccounting = dto.ResponsibleAccounting;
        contract.ResponsiblePricing = dto.ResponsiblePricing;
        contract.CurrencyId = dto.CurrencyId;
        contract.Notes = dto.Notes;
        contract.UpdatedAt = DateTime.UtcNow;
        contract.UpdatedBy = "System"; // TODO: Aus Authentication holen

        await _context.SaveChangesAsync();

        _logger.LogInformation("Contract {ContractNumber} updated", contract.ContractNumber);

        // Reload für Response
        contract = await _context.Contracts
            .Include(c => c.Customer)
            .Include(c => c.Mandant)
            .Include(c => c.ContractGroup)
            .Include(c => c.Currency)
            .FirstAsync(c => c.Id == id);

        return Ok(MapToDto(contract));
    }

    /// <summary>
    /// Lösche einen Vertrag
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContract(Guid id)
    {
        _logger.LogInformation("Deleting contract {ContractId}", id);

        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
        {
            return NotFound(new { message = $"Vertrag mit ID {id} nicht gefunden" });
        }

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Contract {ContractNumber} deleted", contract.ContractNumber);

        return NoContent();
    }

    // Helper Methods
    private ContractDto MapToDto(Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            ContractNumber = contract.ContractNumber,
            CustomerId = contract.CustomerId,
            CustomerName = contract.Customer?.Name ?? "",
            CustomerNumber = contract.Customer?.CustomerNumber ?? "",
            MandantId = contract.MandantId,
            MandantName = contract.Mandant?.Name ?? "",
            ContractGroupId = contract.ContractGroupId,
            ContractGroupName = contract.ContractGroup?.Name ?? "",
            ContractType = contract.ContractType,
            ContractTypeDisplay = contract.ContractType.ToString(),
            Status = contract.Status,
            StatusDisplay = contract.Status switch
            {
                ContractStatus.Active => "Aktiv",
                ContractStatus.InNegotiation => "In Verhandlung",
                ContractStatus.Terminated => "Gekündigt",
                ContractStatus.Ended => "Beendet",
                ContractStatus.Suspended => "Ausgesetzt",
                _ => contract.Status.ToString()
            },
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            IsUnlimited = contract.IsUnlimited,
            NoticePeriodMonths = contract.NoticePeriodMonths,
            NoticeDeadline = contract.NoticeDeadline,
            AutoRenew = contract.AutoRenew,
            BillingStartDate = contract.BillingStartDate,
            ResponsibleSales = contract.ResponsibleSales,
            ResponsibleAccounting = contract.ResponsibleAccounting,
            ResponsiblePricing = contract.ResponsiblePricing,
            CurrencyId = contract.CurrencyId,
            CurrencyCode = contract.Currency?.Code ?? "",
            Notes = contract.Notes,
            CreatedAt = contract.CreatedAt,
            CreatedBy = contract.CreatedBy,
            UpdatedAt = contract.UpdatedAt,
            UpdatedBy = contract.UpdatedBy
        };
    }
}
