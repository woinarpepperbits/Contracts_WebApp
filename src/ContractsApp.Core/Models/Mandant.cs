namespace ContractsApp.Core.Models;

/// <summary>
/// Mandant
/// </summary>
public class Mandant : BaseEntity
{
    /// <summary>
    /// Name des Mandanten
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
    /// Verträge dieses Mandanten
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
