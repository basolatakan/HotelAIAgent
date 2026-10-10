using HotelAIAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Rule> Rules { get; set; } = null!;
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API configurations

        // Customer unique phone number
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        // Hotel - Room relationship (1-N)
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Hotel - Rule relationship (1-N)
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rules)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tenant - Hotel relationship (1-N)
        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Hotels)
            .WithOne(h => h.Tenant)
            .HasForeignKey(h => h.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Conversation - Offer relationship
        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Conversation)
            .WithMany(c => c.Offers)
            .HasForeignKey(o => o.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Room - Offer relationship
        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Room)
            .WithMany(r => r.Offers)
            .HasForeignKey(o => o.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

        // Precision for decimals
        modelBuilder.Entity<Room>()
            .Property(r => r.BasePrice)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Room>()
            .Property(r => r.FloorPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Rule>()
            .Property(r => r.DiscountPercentage)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Offer>()
            .Property(o => o.OriginalPrice)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Offer>()
            .Property(o => o.OfferedPrice)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Offer>()
            .Property(o => o.DiscountApplied)
            .HasColumnType("decimal(18,2)");
    }
}
