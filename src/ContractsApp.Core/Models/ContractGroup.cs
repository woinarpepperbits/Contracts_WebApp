namespace ContractsApp.Core.Models;

/// <summary>
/// Vertragsgruppe zur Kategorisierung von Verträgen
/// </summary>
public class ContractGroup : BaseEntity
{
    /// <summary>
    /// Name der Vertragsgruppe
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Kurz-Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Beschreibung
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Aktiv
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Verträge in dieser Gruppe
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
