using HotelAIAgent.Application.Dtos;
using HotelAIAgent.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAIAgent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentController : ControllerBase
{
    private readonly AgentOrchestrator _agentOrchestrator;

    public AgentController(AgentOrchestrator agentOrchestrator)
    {
        _agentOrchestrator = agentOrchestrator;
    }

    [HttpPost("message")]
    public async Task<IActionResult> SendMessage([FromBody] MessageRequestDto request)
    {
        // TenantResolverMiddleware tarafından set edilen HotelId'yi al
        if (!HttpContext.Items.TryGetValue("HotelId", out var hotelIdObj) || hotelIdObj is null)
        {
            return BadRequest("HotelId context içerisinde bulunamadı.");
        }

        var hotelId = (Guid)hotelIdObj;

        try
        {
            var response = await _agentOrchestrator.ProcessMessageAsync(hotelId, request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Loglama eklenebilir
            return StatusCode(500, new { Error = ex.Message });
        }
    }
}
