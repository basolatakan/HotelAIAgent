using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Enums;
using HotelAIAgent.Domain.Repositories;

namespace HotelAIAgent.Application.Services;

public class RuleEngine
{
    private readonly IRuleRepository _ruleRepository;

    public RuleEngine(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }

    /// <summary>
    /// Müşteri için uygun kuralları değerlendirir ve odanın FloorPrice sınırını gözeterek 
    /// verebileceği en iyi teklifli fiyatı hesaplar.
    /// </summary>
    public async Task<(decimal OfferedPrice, decimal DiscountApplied, Guid? AppliedRuleId, bool Escalated)> CalculateOfferAsync(
        Guid hotelId,
        Room room,
        decimal currentOfferPrice,
        int pastVisitCount,
        CancellationToken cancellationToken = default)
    {
        var rules = await _ruleRepository.GetActiveRulesByHotelIdAsync(hotelId, cancellationToken);
        
        decimal bestDiscountPercent = 0;
        Guid? appliedRuleId = null;

        // Kuralları değerlendir (Priority sırasına göre geldiği varsayılıyor)
        foreach (var rule in rules)
        {
            if (rule.RuleType == RuleType.RepeatVisitCount)
            {
                if (EvaluateCondition(rule.ConditionOperator, rule.ConditionValue, pastVisitCount))
                {
                    if (rule.DiscountPercentage > bestDiscountPercent)
                    {
                        bestDiscountPercent = rule.DiscountPercentage;
                        appliedRuleId = rule.Id;
                    }
                }
            }
            // Faz 1'de sadece RepeatVisitCount destekleniyor. 
            // Diğer kurallar ileride buraya eklenecek.
        }

        // Eğer geçerli bir kural bulunamadıysa (veya indirim 0 ise), mevcut teklifi koru
        if (bestDiscountPercent == 0)
        {
            return (currentOfferPrice, 0, null, false);
        }

        // BasePrice üzerinden indirim hesapla (veya mevcut teklif üzerinden, stratejiye göre değişir.
        // Genelde indirim BasePrice üzerinden hesaplanır.)
        decimal calculatedPrice = room.BasePrice - (room.BasePrice * (bestDiscountPercent / 100m));

        // FloorPrice kontrolü (Anti-Loss Protection)
        bool escalated = false;
        if (calculatedPrice < room.FloorPrice)
        {
            calculatedPrice = room.FloorPrice; // FloorPrice'a sabitle
            escalated = true; // Sınır aşıldı, satış ekibine uyarı gitmeli
        }

        return (calculatedPrice, room.BasePrice - calculatedPrice, appliedRuleId, escalated);
    }

    private bool EvaluateCondition(string conditionOperator, string conditionValue, int actualValue)
    {
        if (!int.TryParse(conditionValue, out int expectedValue))
            return false;

        return conditionOperator.ToLowerInvariant() switch
        {
            "greaterthan" => actualValue > expectedValue,
            "greaterthanorequal" => actualValue >= expectedValue,
            "lessthan" => actualValue < expectedValue,
            "lessthanorequal" => actualValue <= expectedValue,
            "equals" => actualValue == expectedValue,
            _ => false
        };
    }
}
