namespace ContractsApp.Core.Models;

/// <summary>
/// Preisart (Arbeits-/Grundpreis, Netznutzung, etc.)
/// </summary>
public class PriceType : BaseEntity
{
    /// <summary>
    /// Name der Preisart
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Beschreibung
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Standard-Einheit
    /// </summary>
    public string DefaultUnit { get; set; } = string.Empty;

    /// <summary>
    /// Aktiv
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Vertragspreise mit dieser Preisart
    /// </summary>
    public ICollection<ContractPrice> ContractPrices { get; set; } = new List<ContractPrice>();
}
