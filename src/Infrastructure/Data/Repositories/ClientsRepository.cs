using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data.Entities;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public class ClientsRepository : IAsyncRepository<Client>
    {
        private readonly ClientContext _context;

        public ClientsRepository(ClientContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Client>> GetAll()
        {
            return await _context.Clients
                .ToArrayAsync();
        }

        public async Task<Client> GetById(Guid clientId)
        {
            return await _context.Clients
                .Where(client => client.Id == clientId)
                .FirstOrDefaultAsync();
        }

        public async Task<Client> Add(Client entity)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            await _context.Clients
                .AddAsync(entity, token);

            await _context.SaveChangesAsync(token);

            return entity;
        }

        public async Task<Client> Update(Client entity, Guid clientId)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var client = await _context.Clients
                .FindAsync(clientId);

            client.Address = entity.Address;

            _context.Clients
                .Update(client);

            await _context.SaveChangesAsync(token);

            return entity;
        }
    }
}