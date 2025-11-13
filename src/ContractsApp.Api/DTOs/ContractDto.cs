using ContractsApp.Core.Models.Enums;

namespace ContractsApp.Api.DTOs;

/// <summary>
/// DTO für die Rückgabe eines Vertrags (Read)
/// </summary>
public class ContractDto
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerNumber { get; set; } = string.Empty;
    public Guid MandantId { get; set; }
    public string MandantName { get; set; } = string.Empty;
    public Guid ContractGroupId { get; set; }
    public string ContractGroupName { get; set; } = string.Empty;
    public ContractType ContractType { get; set; }
    public string ContractTypeDisplay { get; set; } = string.Empty;
    public ContractStatus Status { get; set; }
    public string StatusDisplay { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsUnlimited { get; set; }
    public int NoticePeriodMonths { get; set; }
    public DateTime? NoticeDeadline { get; set; }
    public bool AutoRenew { get; set; }
    public DateTime BillingStartDate { get; set; }
    public string ResponsibleSales { get; set; } = string.Empty;
    public string ResponsibleAccounting { get; set; } = string.Empty;
    public string ResponsiblePricing { get; set; } = string.Empty;
    public Guid CurrencyId { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}
