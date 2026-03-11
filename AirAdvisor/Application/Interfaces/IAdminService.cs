using Graduation_Project.Application.DTOs.Analytics;

namespace Graduation_Project.Application.Interfaces;

public interface IAdminService
{
    Task<DashboardDto> GetDashboardAsync();
    Task<IEnumerable<UserInfoDto>> GetAllUsersAsync();
    Task<bool> DeleteUserAsync(string userId);
}

