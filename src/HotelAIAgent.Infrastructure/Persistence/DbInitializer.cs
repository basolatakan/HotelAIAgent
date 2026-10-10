using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Yalnızca DB boşsa seed yap
        if (await context.Tenants.AnyAsync())
            return;

        // 1. Tenant oluştur
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = "Kaya Turizm Grubu",
            Email = "info@kayaturizm.com",
            Phone = "+902120000000"
        };
        await context.Tenants.AddAsync(tenant);

        // 2. Hotel oluştur
        var hotel = new Hotel
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            Name = "Kaya Belek Resort",
            Address = "Belek, Antalya",
            PmsProvider = PmsProviderType.Mock,
            InventoryProvider = InventoryProviderType.Mock,
            AdminApiKey = "admin-secret-key-123"
        };
        await context.Hotels.AddAsync(hotel);

        // 3. Odalar oluştur
        var room1 = new Room
        {
            Id = Guid.NewGuid(),
            HotelId = hotel.Id,
            RoomType = "Standart Oda",
            BasePrice = 1000m,
            FloorPrice = 800m,
            IsAvailable = true
        };
        
        var room2 = new Room
        {
            Id = Guid.NewGuid(),
            HotelId = hotel.Id,
            RoomType = "Deluxe Deniz Manzaralı",
            BasePrice = 2000m,
            FloorPrice = 1600m,
            IsAvailable = true
        };
        await context.Rooms.AddRangeAsync(room1, room2);

        // 4. Kurallar oluştur
        var rule1 = new Rule
        {
            Id = Guid.NewGuid(),
            HotelId = hotel.Id,
            RuleType = RuleType.RepeatVisitCount,
            ConditionOperator = "GreaterThanOrEqual",
            ConditionValue = "3",
            DiscountPercentage = 30m,
            OfferValidityHours = 24,
            IsActive = true,
            Priority = 1
        };

        var rule2 = new Rule
        {
            Id = Guid.NewGuid(),
            HotelId = hotel.Id,
            RuleType = RuleType.RepeatVisitCount,
            ConditionOperator = "GreaterThanOrEqual",
            ConditionValue = "2",
            DiscountPercentage = 10m,
            OfferValidityHours = 24,
            IsActive = true,
            Priority = 2
        };
        await context.Rules.AddRangeAsync(rule1, rule2);

        // 5. Mock Müşteri (Test için geçmişi olan müşteri simülasyonu)
        var customer1 = new Customer
        {
            Id = Guid.NewGuid(),
            PhoneNumber = "+905551112233",
            FullName = "Ahmet Yılmaz",
            Email = "ahmet@example.com"
        };
        await context.Customers.AddAsync(customer1);

        await context.SaveChangesAsync();
    }
}
