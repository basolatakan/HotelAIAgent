using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Tüm entity repository'leri için jenerik temel arayüz.
/// Standart CRUD işlemlerini tanımlar. Her entity-specific repository
/// bu arayüzü miras alarak özelleştirilmiş sorgular ekleyebilir.
/// </summary>
/// <typeparam name="T">BaseEntity'den türeyen herhangi bir entity tipi.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>ID ile tek bir entity getirir.</summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Tüm entity'leri listeler.</summary>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Yeni bir entity ekler.</summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Mevcut bir entity'yi günceller.</summary>
    void Update(T entity);

    /// <summary>Bir entity'yi siler.</summary>
    void Delete(T entity);
}
