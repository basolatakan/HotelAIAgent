using Microsoft.AspNetCore.Http;

namespace HotelAIAgent.API.Middleware;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. Header'dan tenant bilgisini al ("X-Hotel-ID" vb.)
        if (context.Request.Headers.TryGetValue("X-Hotel-Id", out var hotelIdHeader))
        {
            if (Guid.TryParse(hotelIdHeader.ToString(), out var hotelId))
            {
                // HttpContext Items içerisine hotelId bilgisini ekle
                context.Items["HotelId"] = hotelId;
            }
        }
        else if (context.Request.Path.StartsWithSegments("/api/agent"))
        {
            // Eğer Agent endpointine istek geliyorsa ve X-Hotel-Id yoksa BadRequest dönülebilir.
            // Fakat sadece bir log yazıp devam da edilebilir. MVP'de zorunlu kılıyoruz.
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("X-Hotel-Id header is missing.");
            return;
        }

        await _next(context);
    }
}
