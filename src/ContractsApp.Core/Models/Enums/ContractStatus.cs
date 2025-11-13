namespace ContractsApp.Core.Models.Enums;

/// <summary>
/// Status eines Vertrags
/// </summary>
public enum ContractStatus
{
    /// <summary>Vertrag ist in Verhandlung</summary>
    InNegotiation = 0,
    
    /// <summary>Vertrag ist aktiv</summary>
    Active = 1,
    
    /// <summary>Vertrag wurde gekündigt</summary>
    Terminated = 2,
    
    /// <summary>Vertrag ist beendet</summary>
    Ended = 3,
    
    /// <summary>Vertrag ist ausgesetzt</summary>
    Suspended = 4
}
