namespace ContractsApp.Core.Models;

/// <summary>
/// Währung
/// </summary>
public class Currency : BaseEntity
{
    /// <summary>
    /// ISO-Code (z.B. EUR, USD)
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Name der Währung
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Symbol (z.B. €, $)
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Aktiv
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Verträge mit dieser Währung
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
