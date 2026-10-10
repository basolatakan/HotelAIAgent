namespace HotelAIAgent.Application.Dtos;

public class MessageRequestDto
{
    /// <summary>Eğer konuşma daha önce başladıysa gönderilir.</summary>
    public Guid? ConversationId { get; set; }

    /// <summary>Müşteriyi tanımlayan eşsiz numara.</summary>
    public string CustomerPhone { get; set; } = string.Empty;

    /// <summary>Müşteri tam adı (Eğer yeni müşteri ise kullanılır).</summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>Gelen doğal dil metni.</summary>
    public string MessageText { get; set; } = string.Empty;
}
