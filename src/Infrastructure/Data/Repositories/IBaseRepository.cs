using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Enums;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public interface IBaseRepository<T, E> where T : class where E : struct
    {
        Task<IReadOnlyCollection<T>> GetAll();

        Task<IReadOnlyCollection<T>> GetAllByStatus(E status);

        Task<T> GetById(Guid Id);

        Task<T> Add(T model);

        Task<T> Update(T model, Guid Id);
    }
}