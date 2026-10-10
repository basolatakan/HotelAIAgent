using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Customer entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>Telefon numarasına göre müşteri arar.</summary>
    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
}
