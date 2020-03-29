using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<IReadOnlyCollection<TEntity>> GetAll();

        Task<TEntity> GetById(Guid entityId);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity, Guid entityId);

        Task<TEntity> Remove(Guid entityId);
    }

    public interface IAsyncRepository<TEntity, TEnum> : IAsyncRepository<TEntity>
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct
    {
        Task<IReadOnlyCollection<TEntity>> GetAllByStatus(TEnum entityStatus);
    }
}