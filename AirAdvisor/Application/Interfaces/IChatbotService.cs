using Graduation_Project.Application.DTOs;

namespace Graduation_Project.Application.Interfaces;

public interface IChatbotService
{
    Task<ChatMessageDto> SendMessageAsync(string userId, SendChatMessageDto dto);
    Task<IEnumerable<ChatMessageDto>> GetUserHistoryAsync(string userId);
}

