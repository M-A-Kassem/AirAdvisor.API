using AutoMapper;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;

namespace Graduation_Project.Application.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoomCalculationResponseDto> CalculateAsync(string userId, RoomCalculationRequestDto dto)
    {
        var volume = dto.Length * dto.Width * dto.Height;
        var multiplier = dto.ThermalFactor ? 300.0 : 250.0;
        var coolingLoad = volume * multiplier;
        var recommended = GetRecommendedCapacity(coolingLoad);

        var calculation = new RoomCalculation
        {
            UserId = userId,
            Length = dto.Length,
            Width = dto.Width,
            Height = dto.Height,
            HasThermalFactor = dto.ThermalFactor,
            RoomVolume = volume,
            CoolingLoad = coolingLoad,
            RecommendedCapacity = recommended,
            CalculatedAt = DateTime.UtcNow
        };

        await _unitOfWork.RoomCalculations.AddAsync(calculation);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<RoomCalculationResponseDto>(calculation);
    }

    public async Task<IEnumerable<RoomCalculationResponseDto>> GetUserCalculationsAsync(string userId)
    {
        var calculations = await _unitOfWork.RoomCalculations.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<RoomCalculationResponseDto>>(calculations);
    }

    private static string GetRecommendedCapacity(double coolingLoad)
    {
        return coolingLoad switch
        {
            <= 12000 => "You should choose a 1.5 HP AC (12,000 BTU)",
            <= 18000 => "You should choose a 2.25 HP AC (18,000 BTU)",
            <= 24000 => "You should choose a 3 HP AC (24,000 BTU)",
            <= 36000 => "You should choose a 4 HP AC (32,000 - 36,000 BTU)",
            <= 48000 => "You should choose a 5 HP AC (40,000 - 48,000 BTU)",
            _ => $"Your cooling load is {coolingLoad:N0} BTU — you may need multiple AC units"
        };
    }
}

