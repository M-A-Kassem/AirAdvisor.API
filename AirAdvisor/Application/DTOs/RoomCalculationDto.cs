using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Application.DTOs;

public class RoomCalculationRequestDto
{
    [Required, Range(0.1, double.MaxValue)]
    public double Length { get; set; }

    [Required, Range(0.1, double.MaxValue)]
    public double Width { get; set; }

    [Required, Range(0.1, double.MaxValue)]
    public double Height { get; set; }

    [Required]
    public bool ThermalFactor { get; set; }
}

public class RoomCalculationResponseDto
{
    public int Id { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public bool HasThermalFactor { get; set; }
    public double RoomVolume { get; set; }
    public double CoolingLoad { get; set; }
    public string RecommendedCapacity { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; }
}

