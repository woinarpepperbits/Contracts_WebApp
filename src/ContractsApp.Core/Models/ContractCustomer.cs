using ContractsApp.Core.Models.Enums;

namespace ContractsApp.Core.Models;

/// <summary>
/// Vertragskunde - Zuordnung von Kunden zu Verträgen mit zusätzlichen Informationen
/// </summary>
public class ContractCustomer : BaseEntity
{
    /// <summary>
    /// Zugehöriger Vertrag
    /// </summary>
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    /// <summary>
    /// Zugehöriger Kunde
    /// </summary>
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    /// <summary>
    /// Kundennummer im Kontext des Vertrags
    /// </summary>
    public string CustomerNumber { get; set; } = string.Empty;

    /// <summary>
    /// Rolle des Kunden
    /// </summary>
    public ContractCustomerRole Role { get; set; } = ContractCustomerRole.ContractPartner;

    /// <summary>
    /// Abschlagsbetrag (monatlich)
    /// </summary>
    public decimal AdvancePaymentAmount { get; set; }

    /// <summary>
    /// Abschlagszyklus (Monate)
    /// </summary>
    public int AdvancePaymentCycle { get; set; } = 1;

    /// <summary>
    /// Zahlungskonditionen (Text)
    /// </summary>
    public string PaymentTerms { get; set; } = string.Empty;

    /// <summary>
    /// FIBU-Referenz
    /// </summary>
    public string AccountingReference { get; set; } = string.Empty;
}
