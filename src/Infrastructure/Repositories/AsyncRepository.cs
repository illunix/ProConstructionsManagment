using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        public AsyncRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public async Task<IReadOnlyCollection<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>()
                .ToArrayAsync();
        }

        public async Task<TEntity> GetById(Guid entityId)
        {
            return await _context.Set<TEntity>()
                .Where(entity => entity.Id == entityId)
                .FirstOrDefaultAsync();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>()
                .Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity, Guid entityId)
        {
            var entityToUpdate = await _context.Set<TEntity>()
                .FindAsync(entityId);
            
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Remove(Guid entityId)
        {
            var entity = await _context.Set<TEntity>()
                .FindAsync(entityId);

            _context.Set<TEntity>()
                .Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }

    public class AsyncRepository<TEntity, TEnum> : AsyncRepository<TEntity>, IAsyncRepository<TEntity, TEnum>
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct
    {
        private readonly ApplicationDbContext _context;

        public AsyncRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllByStatus(TEnum entityStatus)
        {
            return await _context.Set<TEntity>()
                .Where(entity => entity.Status.Equals(entityStatus))
                .ToArrayAsync();
        }
    }
}