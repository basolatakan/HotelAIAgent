using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Application.Services;

/// <summary>
/// ConversationState ve ConversationEvent arasındaki geçerli durum geçişlerini yönetir.
/// </summary>
public class StateMachine
{
    public ConversationState GetNextState(ConversationState currentState, ConversationEvent triggeredEvent)
    {
        return (currentState, triggeredEvent) switch
        {
            // Başlangıç durumu geçişleri
            (ConversationState.Started, ConversationEvent.CustomerIdentified) => ConversationState.InProgress,
            (ConversationState.Started, ConversationEvent.RoomInquiry) => ConversationState.InProgress,

            // İşlemde durumu geçişleri
            (ConversationState.InProgress, ConversationEvent.RoomInquiry) => ConversationState.OfferSent, // Sistem teklif hazırladı ve gönderdi

            // Teklif sunulduktan sonraki geçişler
            (ConversationState.OfferSent, ConversationEvent.BargainRequested) => ConversationState.OfferSent, // Pazarlık devam ediyor, state değişmez
            (ConversationState.OfferSent, ConversationEvent.OfferAccepted) => ConversationState.Accepted,
            (ConversationState.OfferSent, ConversationEvent.OfferRejected) => ConversationState.Rejected,

            // Geçersiz veya tanımlanmamış bir geçiş ise durumu değiştirme
            _ => currentState
        };
    }
}
