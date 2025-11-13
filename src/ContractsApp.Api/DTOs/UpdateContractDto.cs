using ContractsApp.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContractsApp.Api.DTOs;

/// <summary>
/// DTO für das Aktualisieren eines Vertrags
/// </summary>
public class UpdateContractDto
{
    [Required(ErrorMessage = "Vertragsnummer ist erforderlich")]
    [StringLength(50)]
    public string ContractNumber { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }
    public Guid MandantId { get; set; }
    public Guid ContractGroupId { get; set; }
    public ContractType ContractType { get; set; }
    public ContractStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsUnlimited { get; set; }
    
    [Range(0, 120)]
    public int NoticePeriodMonths { get; set; }
    
    public DateTime? NoticeDeadline { get; set; }
    public bool AutoRenew { get; set; }
    public DateTime BillingStartDate { get; set; }
    
    [StringLength(100)]
    public string ResponsibleSales { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string ResponsibleAccounting { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string ResponsiblePricing { get; set; } = string.Empty;
    
    public Guid CurrencyId { get; set; }
    
    [StringLength(2000)]
    public string Notes { get; set; } = string.Empty;
}
