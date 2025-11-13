namespace ContractsApp.Core.Models.Enums;

/// <summary>
/// Rolle des Kunden im Vertrag
/// </summary>
public enum ContractCustomerRole
{
    /// <summary>Vertragspartner</summary>
    ContractPartner = 0,
    
    /// <summary>Rechnungsempfänger (abweichend)</summary>
    InvoiceRecipient = 1,
    
    /// <summary>Beides</summary>
    Both = 2
}
