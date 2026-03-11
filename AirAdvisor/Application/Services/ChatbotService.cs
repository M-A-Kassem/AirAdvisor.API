using AutoMapper;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;

namespace Graduation_Project.Application.Services;

public class ChatbotService : IChatbotService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChatbotService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ChatMessageDto> SendMessageAsync(string userId, SendChatMessageDto dto)
    {
        var botResponse = GenerateResponse(dto.Message);

        var chatMessage = new ChatMessage
        {
            UserId = userId,
            UserMessage = dto.Message,
            BotResponse = botResponse,
            Timestamp = DateTime.UtcNow
        };

        await _unitOfWork.ChatMessages.AddAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ChatMessageDto>(chatMessage);
    }

    public async Task<IEnumerable<ChatMessageDto>> GetUserHistoryAsync(string userId)
    {
        var messages = await _unitOfWork.ChatMessages.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ChatMessageDto>>(messages);
    }

    private static string GenerateResponse(string message)
    {
        var lower = message.ToLower().Trim();

        if (ContainsAny(lower, "hello", "hi", "hey", "greetings"))
            return "Hello! Welcome to our AC Store. How can I help you today? You can ask about products, room calculations, or purchasing.";

        if (ContainsAny(lower, "product", "air conditioner", "ac unit", "what do you sell", "catalog"))
            return "We sell a wide range of Air Conditioners from top brands. Use the Products endpoint (GET /api/product) to browse all available AC units with their cooling capacities, prices, and specifications.";

        if (ContainsAny(lower, "brand"))
            return "We carry AC units from multiple brands. You can filter products by brand using GET /api/product/brand/{brandName}.";

        if (ContainsAny(lower, "room", "calculate", "capacity", "cooling", "btu", "size", "what ac do i need", "which ac"))
            return "To find the right AC for your room, use the Room Calculator (POST /api/room/calculate). Provide your room's Length, Width, Height, and whether it has a thermal factor (sun exposure or top floor). We'll calculate the cooling load and recommend the right AC capacity for you!";

        if (ContainsAny(lower, "thermal", "sun", "heat", "top floor", "insulation"))
            return "Thermal factor applies if your room has direct sun exposure on walls or is on the top floor. When thermal factor is true, we use a multiplier of 300 (instead of 250) to calculate a higher cooling load, ensuring adequate cooling.";

        if (ContainsAny(lower, "buy", "purchase", "order", "how to buy"))
            return "To purchase an AC: 1) Browse products (GET /api/product), 2) Calculate your room's needs (POST /api/room/calculate), 3) Place an order (POST /api/sales) with the ProductId and Quantity. The price is calculated automatically!";

        if (ContainsAny(lower, "price", "cost", "how much", "expensive", "cheap", "affordable"))
            return "Our AC prices vary based on brand, model, and cooling capacity. Browse all products at GET /api/product to see current prices. Higher capacity units are generally more expensive.";

        if (ContainsAny(lower, "history", "my order", "my purchase", "past order"))
            return "You can view your purchase history at GET /api/sales/my. It shows all your past orders including product details, quantities, and total prices.";

        if (ContainsAny(lower, "stock", "available", "in stock"))
            return "Each product listing shows the available StockQuantity. If a product is out of stock, you won't be able to purchase it until it's restocked.";

        if (ContainsAny(lower, "ton", "btu"))
            return "AC capacity is measured in BTU or Tons. 1 Ton = 12,000 BTU. Common sizes: 0.75 Ton (9,000 BTU), 1 Ton (12,000 BTU), 1.5 Ton (18,000 BTU), 2 Ton (24,000 BTU), 3 Ton (36,000 BTU).";

        if (ContainsAny(lower, "help", "support", "what can you do"))
            return "I can help you with: 1) Browsing AC products, 2) Calculating the right AC size for your room, 3) Explaining how to purchase, 4) Answering questions about AC capacity/BTU, 5) Checking your order history. Just ask!";

        if (ContainsAny(lower, "thank", "thanks"))
            return "You're welcome! Feel free to ask if you have any other questions about our Air Conditioners.";

        if (ContainsAny(lower, "bye", "goodbye"))
            return "Goodbye! Thank you for visiting our AC Store. Come back anytime!";

        return "I'm your AC Store assistant. I can help with: browsing products, room AC calculations, purchasing, and general AC questions. Could you please rephrase your question?";
    }

    private static bool ContainsAny(string text, params string[] keywords)
        => keywords.Any(k => text.Contains(k));
}

