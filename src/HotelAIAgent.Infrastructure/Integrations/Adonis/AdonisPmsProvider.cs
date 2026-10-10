using HotelAIAgent.Application.Interfaces;

namespace HotelAIAgent.Infrastructure.Integrations.Adonis;

public class AdonisPmsProvider : IPmsService
{
    public Task<int> GetCustomerPastVisitCountAsync(Guid hotelId, string phoneNumber, CancellationToken cancellationToken = default)
    {
        // TODO: Faz 2 veya 3'te gerçek Adonis API entegrasyonu buraya yazılacak.
        // Şimdilik NotSupportedException fırlatarak kullanımını engelliyoruz.
        throw new NotSupportedException("Canlı Adonis entegrasyonu henüz MVP aşamasında aktif değildir. Lütfen PmsProviderType.Mock kullanın.");
    }
}
