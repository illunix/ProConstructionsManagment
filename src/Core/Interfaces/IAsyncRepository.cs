using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<T> Add(T entity);
        Task<T> Update(T entity, Guid Id);
    }

    public interface IAsyncRepository<T, E> where T : class where E : struct
    {
        Task<IReadOnlyCollection<T>> GetAll();

        Task<IReadOnlyCollection<T>> GetAllByStatus(E status);

        Task<T> GetById(Guid Id);

        Task<T> Add(T entity);

        Task<T> Update(T entity, Guid Id);
    }
}