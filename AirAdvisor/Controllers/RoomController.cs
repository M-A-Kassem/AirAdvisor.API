using System.Security.Claims;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] RoomCalculationRequestDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _roomService.CalculateAsync(userId, dto);
        return Ok(result);
    }

    [HttpGet("my-calculations")]
    public async Task<IActionResult> GetMyCalculations()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var calculations = await _roomService.GetUserCalculationsAsync(userId);
        return Ok(calculations);
    }
}

