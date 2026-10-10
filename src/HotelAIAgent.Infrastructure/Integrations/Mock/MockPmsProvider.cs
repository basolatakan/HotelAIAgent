using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Domain.Repositories;

namespace HotelAIAgent.Infrastructure.Integrations.Mock;

public class MockPmsProvider : IPmsService
{
    private readonly ICustomerRepository _customerRepository;

    public MockPmsProvider(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> GetCustomerPastVisitCountAsync(Guid hotelId, string phoneNumber, CancellationToken cancellationToken = default)
    {
        // Mock senaryosu:
        // Eğer müşteri veri tabanında (DbInitializer ile seed edilmiş) bulunuyorsa,
        // test amaçlı olarak geçmişte 3 kez konakladığını varsayalım.
        // Eğer yoksa yeni müşteri olarak 0 döneriz.
        
        var customer = await _customerRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);
        
        if (customer != null)
        {
            // Seed datada "+905551112233" numaralı Ahmet Yılmaz var.
            // O geldiğinde 3 ziyaretçi indirimini (kuralı) test edebilmemiz için 3 dönüyoruz.
            return 3;
        }

        return 0;
    }
}
