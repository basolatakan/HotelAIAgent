using HotelAIAgent.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelAIAgent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelRepository _hotelRepository;

    public HotelsController(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> GetHotelsByTenant(Guid tenantId)
    {
        var hotels = await _hotelRepository.GetByTenantIdAsync(tenantId);
        return Ok(hotels);
    }
}
