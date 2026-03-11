namespace Graduation_Project.Domain.Entities;

public class RoomCalculation
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public bool HasThermalFactor { get; set; }
    public double RoomVolume { get; set; }
    public double CoolingLoad { get; set; }
    public string RecommendedCapacity { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}

