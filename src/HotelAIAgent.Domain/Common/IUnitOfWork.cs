namespace HotelAIAgent.Domain.Common;

/// <summary>
/// Unit of Work pattern arayüzü.
/// Birden fazla repository işlemini tek bir transaction'da kaydetmeyi sağlar.
/// SaveChangesAsync çağrılana kadar hiçbir değişiklik veri tabanına yansımaz.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Bekleyen tüm değişiklikleri veri tabanına tek bir transaction olarak kaydeder.
    /// </summary>
    /// <param name="cancellationToken">İptal belirteci.</param>
    /// <returns>Etkilenen kayıt sayısı.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
