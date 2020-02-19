using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IClientsService
    {
        Task<ObservableCollection<Client>> GetAllClients();
        Task<ObservableCollection<Client>> GetClientById(Guid clientId);
    }
}