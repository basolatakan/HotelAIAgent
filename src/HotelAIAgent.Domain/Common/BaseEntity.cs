namespace HotelAIAgent.Domain.Common;

/// <summary>
/// Tüm entity'lerin miras aldığı temel sınıf.
/// Ortak alanları (Id, oluşturma/güncelleme tarihleri) merkezi olarak yönetir.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>Benzersiz tanımlayıcı (Primary Key).</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Kaydın oluşturulma tarihi (UTC).</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Kaydın son güncellenme tarihi (UTC).</summary>
    public DateTime? UpdatedAt { get; set; }
}
