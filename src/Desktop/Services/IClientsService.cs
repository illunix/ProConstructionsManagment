using ProConstructionsManagment.Desktop.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IClientsService
    {
        Task<ObservableCollection<Client>> GetAllClients();

        Task<Client> GetClientById(string clientId);

        RequestResult<Client> AddClient(Client model);

        RequestResult<Client> UpdateClient(Client model, string clientId);
    }
}