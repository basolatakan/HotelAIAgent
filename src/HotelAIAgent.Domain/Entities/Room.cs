using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Oteldeki bir oda tipini ve fiyat bilgilerini temsil eder.
/// BasePrice standart fiyatı, FloorPrice ise asla altına inilemeyecek
/// taban fiyatı belirler (Anti-Loss Protection).
/// </summary>
public class Room : BaseEntity
{
    /// <summary>Bu odanın ait olduğu otelin ID'si.</summary>
    public Guid HotelId { get; set; }

    /// <summary>Oda tipi (Örn: "Deluxe Deniz Manzaralı", "Standart Oda").</summary>
    public string RoomType { get; set; } = string.Empty;

    /// <summary>Odanın standart gecelik fiyatı (TL).</summary>
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Odanın taban fiyatı (TL). Sistem bu fiyatın altında teklif veremez.
    /// Otel sahibinin belirlediği zarar koruma mekanizmasıdır.
    /// </summary>
    public decimal FloorPrice { get; set; }

    /// <summary>Odanın aktif olarak satışta olup olmadığı.</summary>
    public bool IsAvailable { get; set; } = true;

    // Navigation Properties
    /// <summary>Bu odanın ait olduğu otel.</summary>
    public Hotel Hotel { get; set; } = null!;

    /// <summary>Bu oda için oluşturulan teklifler.</summary>
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
