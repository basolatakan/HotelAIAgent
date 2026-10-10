using HotelAIAgent.API.Middleware;
using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Application.Services;
using HotelAIAgent.Domain.Common;
using HotelAIAgent.Domain.Enums;
using HotelAIAgent.Domain.Repositories;
using HotelAIAgent.Infrastructure.Integrations.Adonis;
using HotelAIAgent.Infrastructure.Integrations.HotelRunner;
using HotelAIAgent.Infrastructure.Integrations.Mock;
using HotelAIAgent.Infrastructure.Persistence;
using HotelAIAgent.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Repositories & UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

// 3. Application Services
builder.Services.AddScoped<EventDetector>();
builder.Services.AddScoped<StateMachine>();
builder.Services.AddScoped<RuleEngine>();
builder.Services.AddScoped<AgentOrchestrator>();

// 4. Integrations - Keyed Services (C# 12 / .NET 8 Feature)
builder.Services.AddKeyedScoped<IPmsService, MockPmsProvider>(PmsProviderType.Mock);
builder.Services.AddKeyedScoped<IPmsService, AdonisPmsProvider>(PmsProviderType.Adonis);

builder.Services.AddKeyedScoped<IInventoryService, MockInventoryProvider>(InventoryProviderType.Mock);
builder.Services.AddKeyedScoped<IInventoryService, HotelRunnerInventoryProvider>(InventoryProviderType.HotelRunner);

builder.Services.AddScoped<IIntegrationFactory, IntegrationFactory>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Uygulama başlarken DB'yi otomatik oluştur/güncelle ve Seed datayı yükle
    context.Database.Migrate();
    await DbInitializer.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use custom tenant resolver middleware
app.UseMiddleware<TenantResolverMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
