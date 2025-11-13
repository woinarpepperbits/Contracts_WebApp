using ContractsApp.Core.Models.Enums;

namespace ContractsApp.Core.Models;

/// <summary>
/// Vertrags-Sonderkunde
/// Hauptentität für die Verwaltung von EVU-Verträgen außerhalb SAP
/// </summary>
public class Contract : BaseEntity
{
    /// <summary>
    /// Eindeutige Vertragsnummer
    /// </summary>
    public string ContractNumber { get; set; } = string.Empty;

    /// <summary>
    /// ID des Kunden
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Navigationseigenschaft zum Kunden
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Mandanten-ID
    /// </summary>
    public Guid MandantId { get; set; }
    
    /// <summary>
    /// Navigationseigenschaft zum Mandanten
    /// </summary>
    public Mandant? Mandant { get; set; }

    /// <summary>
    /// Vertragsgruppen-ID
    /// </summary>
    public Guid ContractGroupId { get; set; }
    
    /// <summary>
    /// Navigationseigenschaft zur Vertragsgruppe
    /// </summary>
    public ContractGroup? ContractGroup { get; set; }

    /// <summary>
    /// Art des Vertrags (Verkauf, Lieferant, etc.)
    /// </summary>
    public ContractType ContractType { get; set; } = ContractType.Sale;

    /// <summary>
    /// Status des Vertrags
    /// </summary>
    public ContractStatus Status { get; set; } = ContractStatus.Active;

    /// <summary>
    /// Vertragsbeginn
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Vertragsende (null = unbefristet)
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Vertrag ist unbefristet
    /// </summary>
    public bool IsUnlimited { get; set; }

    /// <summary>
    /// Kündigungsfrist in Monaten
    /// </summary>
    public int NoticePeriodMonths { get; set; } = 3;

    /// <summary>
    /// Kündigungsstichtag
    /// </summary>
    public DateTime? NoticeDeadline { get; set; }

    /// <summary>
    /// Automatische Verlängerung
    /// </summary>
    public bool AutoRenew { get; set; }

    /// <summary>
    /// Abrechnungsbeginn
    /// </summary>
    public DateTime BillingStartDate { get; set; }

    /// <summary>
    /// Verantwortlicher Sachbearbeiter Vertrieb
    /// </summary>
    public string ResponsibleSales { get; set; } = string.Empty;

    /// <summary>
    /// Verantwortlicher Sachbearbeiter Buchhaltung
    /// </summary>
    public string ResponsibleAccounting { get; set; } = string.Empty;

    /// <summary>
    /// Verantwortlicher Sachbearbeiter Preise
    /// </summary>
    public string ResponsiblePricing { get; set; } = string.Empty;

    /// <summary>
    /// Währungs-ID
    /// </summary>
    public Guid CurrencyId { get; set; }
    
    /// <summary>
    /// Navigationseigenschaft zur Währung
    /// </summary>
    public Currency? Currency { get; set; }

    /// <summary>
    /// Bemerkungen zum Vertrag
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Vertragspreise (Sammlung)
    /// </summary>
    public ICollection<ContractPrice> Prices { get; set; } = new List<ContractPrice>();

    /// <summary>
    /// Vertragskunden (Sammlung)
    /// </summary>
    public ICollection<ContractCustomer> ContractCustomers { get; set; } = new List<ContractCustomer>();
}
