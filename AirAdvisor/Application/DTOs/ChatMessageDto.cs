using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Application.DTOs;

public class ChatMessageDto
{
    public int Id { get; set; }
    public string UserMessage { get; set; } = string.Empty;
    public string BotResponse { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

public class SendChatMessageDto
{
    [Required, MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
}

