namespace HotelAIAgent.Application.Dtos;

public class MessageResponseDto
{
    public Guid ConversationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public string ReplyText { get; set; } = string.Empty;
    public decimal? OfferedPrice { get; set; }
}
