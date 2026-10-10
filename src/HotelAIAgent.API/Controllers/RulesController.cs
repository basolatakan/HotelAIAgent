using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelAIAgent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly IRuleRepository _ruleRepository;

    public RulesController(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }

    [HttpGet("hotel/{hotelId}")]
    public async Task<IActionResult> GetActiveRules(Guid hotelId)
    {
        // Gerçek bir sistemde burada Admin / Yetki kontrolü yapılır.
        var rules = await _ruleRepository.GetActiveRulesByHotelIdAsync(hotelId);
        return Ok(rules);
    }
}
