using ContractsApp.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContractsApp.Api.DTOs;

/// <summary>
/// DTO für das Erstellen eines neuen Vertrags
/// </summary>
public class CreateContractDto
{
    [Required(ErrorMessage = "Vertragsnummer ist erforderlich")]
    [StringLength(50, ErrorMessage = "Vertragsnummer darf maximal 50 Zeichen lang sein")]
    public string ContractNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kunde ist erforderlich")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Mandant ist erforderlich")]
    public Guid MandantId { get; set; }

    [Required(ErrorMessage = "Vertragsgruppe ist erforderlich")]
    public Guid ContractGroupId { get; set; }

    [Required(ErrorMessage = "Vertragsart ist erforderlich")]
    public ContractType ContractType { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Active;

    [Required(ErrorMessage = "Vertragsbeginn ist erforderlich")]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsUnlimited { get; set; }

    [Range(0, 120, ErrorMessage = "Kündigungsfrist muss zwischen 0 und 120 Monaten liegen")]
    public int NoticePeriodMonths { get; set; } = 3;

    public DateTime? NoticeDeadline { get; set; }

    public bool AutoRenew { get; set; }

    [Required(ErrorMessage = "Abrechnungsbeginn ist erforderlich")]
    public DateTime BillingStartDate { get; set; }

    [StringLength(100)]
    public string ResponsibleSales { get; set; } = string.Empty;

    [StringLength(100)]
    public string ResponsibleAccounting { get; set; } = string.Empty;

    [StringLength(100)]
    public string ResponsiblePricing { get; set; } = string.Empty;

    [Required(ErrorMessage = "Währung ist erforderlich")]
    public Guid CurrencyId { get; set; }

    [StringLength(2000)]
    public string Notes { get; set; } = string.Empty;
}
