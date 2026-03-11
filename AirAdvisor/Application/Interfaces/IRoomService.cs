using Graduation_Project.Application.DTOs;

namespace Graduation_Project.Application.Interfaces;

public interface IRoomService
{
    Task<RoomCalculationResponseDto> CalculateAsync(string userId, RoomCalculationRequestDto dto);
    Task<IEnumerable<RoomCalculationResponseDto>> GetUserCalculationsAsync(string userId);
}

