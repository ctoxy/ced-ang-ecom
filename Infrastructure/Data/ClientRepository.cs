using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly StoreContext _context;
        public ClientRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Client> GetClientById(int id)
        {
           return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Client>> GetClientsAsync()
        {
            return await _context.Clients
                .ToListAsync();
        }    

        
    }
}