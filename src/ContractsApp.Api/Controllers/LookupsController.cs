using ContractsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractsApp.Api.Controllers;

/// <summary>
/// Controller für Lookup-Daten (Dropdowns, etc.)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LookupsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LookupsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Hole alle Kunden
    /// </summary>
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _context.Customers
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                c.Id,
                c.CustomerNumber,
                c.Name,
                Display = $"{c.CustomerNumber} - {c.Name}"
            })
            .ToListAsync();

        return Ok(customers);
    }

    /// <summary>
    /// Hole alle Mandanten
    /// </summary>
    [HttpGet("mandants")]
    public async Task<IActionResult> GetMandants()
    {
        var mandants = await _context.Mandants
            .Where(m => m.IsActive)
            .OrderBy(m => m.Name)
            .Select(m => new
            {
                m.Id,
                m.Code,
                m.Name,
                Display = $"{m.Code} - {m.Name}"
            })
            .ToListAsync();

        return Ok(mandants);
    }

    /// <summary>
    /// Hole alle Vertragsgruppen
    /// </summary>
    [HttpGet("contract-groups")]
    public async Task<IActionResult> GetContractGroups()
    {
        var groups = await _context.ContractGroups
            .Where(g => g.IsActive)
            .OrderBy(g => g.Name)
            .Select(g => new
            {
                g.Id,
                g.Code,
                g.Name,
                Display = $"{g.Code} - {g.Name}"
            })
            .ToListAsync();

        return Ok(groups);
    }

    /// <summary>
    /// Hole alle Währungen
    /// </summary>
    [HttpGet("currencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await _context.Currencies
            .Where(c => c.IsActive)
            .OrderBy(c => c.Code)
            .Select(c => new
            {
                c.Id,
                c.Code,
                c.Name,
                c.Symbol,
                Display = $"{c.Code} - {c.Name}"
            })
            .ToListAsync();

        return Ok(currencies);
    }

    /// <summary>
    /// Hole alle Preisarten
    /// </summary>
    [HttpGet("price-types")]
    public async Task<IActionResult> GetPriceTypes()
    {
        var priceTypes = await _context.PriceTypes
            .Where(pt => pt.IsActive)
            .OrderBy(pt => pt.Name)
            .Select(pt => new
            {
                pt.Id,
                pt.Code,
                pt.Name,
                pt.DefaultUnit,
                Display = $"{pt.Code} - {pt.Name}"
            })
            .ToListAsync();

        return Ok(priceTypes);
    }

    /// <summary>
    /// Hole alle Enum-Werte für ContractStatus
    /// </summary>
    [HttpGet("contract-statuses")]
    public IActionResult GetContractStatuses()
    {
        var statuses = new[]
        {
            new { Value = 0, Display = "In Verhandlung" },
            new { Value = 1, Display = "Aktiv" },
            new { Value = 2, Display = "Gekündigt" },
            new { Value = 3, Display = "Beendet" },
            new { Value = 4, Display = "Ausgesetzt" }
        };

        return Ok(statuses);
    }

    /// <summary>
    /// Hole alle Enum-Werte für ContractType
    /// </summary>
    [HttpGet("contract-types")]
    public IActionResult GetContractTypes()
    {
        var types = new[]
        {
            new { Value = 0, Display = "Verkauf" },
            new { Value = 1, Display = "Lieferant" },
            new { Value = 2, Display = "Verkaufschance" }
        };

        return Ok(types);
    }
}
