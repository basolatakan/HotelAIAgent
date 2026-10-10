namespace HotelAIAgent.Application.Interfaces;

/// <summary>
/// PMS (Property Management System) ile etkileşim kuracak servislerin arayüzü.
/// Adonis, Opera vb. PMS sistemleri bu arayüzü uygular.
/// </summary>
public interface IPmsService
{
    /// <summary>
    /// Müşterinin ilgili oteldeki geçmiş konaklama sayısını getirir.
    /// </summary>
    /// <param name="hotelId">Otel ID'si</param>
    /// <param name="phoneNumber">Müşteri telefon numarası</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Geçmiş konaklama sayısı</returns>
    Task<int> GetCustomerPastVisitCountAsync(Guid hotelId, string phoneNumber, CancellationToken cancellationToken = default);
}
