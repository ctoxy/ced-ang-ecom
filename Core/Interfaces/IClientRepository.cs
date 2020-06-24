using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientById(int id);
        Task<IReadOnlyList<Client>> GetClientsAsync();
        
    }
}