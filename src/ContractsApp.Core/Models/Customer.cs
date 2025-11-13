namespace ContractsApp.Core.Models;

/// <summary>
/// Kunde/EVU
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Kundennummer
    /// </summary>
    public string CustomerNumber { get; set; } = string.Empty;

    /// <summary>
    /// Kundenname (Firma)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Adresse
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// PLZ
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Ort
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// E-Mail
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Telefon
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Aktiv
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Verträge dieses Kunden
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
