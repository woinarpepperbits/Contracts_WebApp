namespace ContractsApp.Core.Models;

/// <summary>
/// Vertrags preis mit Gültigkeit
/// </summary>
public class ContractPrice : BaseEntity
{
    /// <summary>
    /// Zugehöriger Vertrag
    /// </summary>
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    /// <summary>
    /// Gültig ab
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// Gültig bis (null = unbegrenzt)
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// Preisart-ID
    /// </summary>
    public Guid PriceTypeId { get; set; }
    public PriceType? PriceType { get; set; }

    /// <summary>
    /// Betrag
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Einheit (z.B. kWh, Jahr, Monat)
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Beschreibung/Bemerkung
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
