namespace Graduation_Project.Application.DTOs.Auth;

public class AuthResponseDto
{
    public bool Succeeded { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}

