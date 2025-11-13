namespace ContractsApp.Core.Models.Enums;

/// <summary>
/// Art des Vertrags
/// </summary>
public enum ContractType
{
    /// <summary>Verkaufsvertrag</summary>
    Sale = 0,
    
    /// <summary>Lieferantenvertrag</summary>
    Supplier = 1,
    
    /// <summary>Verkaufschance</summary>
    SalesOpportunity = 2
}
