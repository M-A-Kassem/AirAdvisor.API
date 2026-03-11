using Graduation_Project.Application.DTOs.Auth;
using Graduation_Project.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IAuthService _authService;
    private readonly ISaleService _saleService;

    public AdminController(IAdminService adminService, IAuthService authService, ISaleService saleService)
    {
        _adminService = adminService;
        _authService = authService;
        _saleService = saleService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard = await _adminService.GetDashboardAsync();
        return Ok(dashboard);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _adminService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _authService.CreateUserAsync(dto);
        if (!result.Succeeded)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await _adminService.DeleteUserAsync(userId);
        return result ? NoContent() : NotFound();
    }

    [HttpGet("sales")]
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleService.GetAllAsync();
        return Ok(sales);
    }

    [HttpGet("sales/user/{userId}")]
    public async Task<IActionResult> GetSalesByUser(string userId)
    {
        var sales = await _saleService.GetByUserAsync(userId);
        return Ok(sales);
    }

    [HttpGet("orders/pending")]
    public async Task<IActionResult> GetPendingOrders()
    {
        var orders = await _saleService.GetPendingOrdersAsync();
        return Ok(orders);
    }

    [HttpPut("orders/{saleId:int}/accept")]
    public async Task<IActionResult> AcceptOrder(int saleId)
    {
        try
        {
            var order = await _saleService.AcceptOrderAsync(saleId);
            return order is null ? NotFound() : Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("orders/{saleId:int}/reject")]
    public async Task<IActionResult> RejectOrder(int saleId)
    {
        try
        {
            var order = await _saleService.RejectOrderAsync(saleId);
            return order is null ? NotFound() : Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

