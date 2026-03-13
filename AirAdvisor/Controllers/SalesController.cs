using System.Security.Claims;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        try
        {
            var sale = await _saleService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetMyPurchases), null, sale);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyPurchases()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var sales = await _saleService.GetUserPurchasesAsync(userId);
        return Ok(sales);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllAsync();
        return Ok(sales);
    }

    // [HttpGet("user/{userId}")]
   // [Authorize(Roles = "Admin")]
   //  public async Task<IActionResult> GetByUser(string userId)
   // {
   //    var sales = await _saleService.GetByUserAsync(userId);
  //     return Ok(sales);
  //   }
}

