using ProConstructionsManagment.Desktop.Configuration;
using ProConstructionsManagment.Desktop.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRequestProvider _requestProvider;

        public ClientsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Client>> GetAllClients()
        {
            var uri = $"{Config.ApiUrlBase}/clients";

            var json = await _requestProvider.GetAsync<RootMultiple<Client>>(uri);

            return json.Data;
        }

        public async Task<Client> GetClientById(string clientId)
        {
            var uri = $"{Config.ApiUrlBase}/clients/{clientId}";

            var json = await _requestProvider.GetAsync<RootSingle<Client>>(uri);

            return json.Data;
        }

        public RequestResult<Client> AddClient(Client model)
        {
            if (string.IsNullOrWhiteSpace(model.CompanyName))
            {
                throw new ArgumentNullException(nameof(model.CompanyName));
            }

            if (string.IsNullOrWhiteSpace(model.NIP))
            {
                throw new ArgumentNullException(nameof(model.NIP));
            }

            if (string.IsNullOrWhiteSpace(model.Address))
            {
                throw new ArgumentNullException(nameof(model.Address));
            }

            try
            {
                var uri = $"{Config.ApiUrlBase}/client/add";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Client>(false);
            }

            return new RequestResult<Client>(true);
        }

        public RequestResult<Client> UpdateClient(Client model, string clientId)
        {
            if (string.IsNullOrWhiteSpace(model.CompanyName))
            {
                throw new ArgumentNullException(nameof(model.CompanyName));
            }

            if (string.IsNullOrWhiteSpace(model.NIP))
            {
                throw new ArgumentNullException(nameof(model.NIP));
            }

            if (string.IsNullOrWhiteSpace(model.Address))
            {
                throw new ArgumentNullException(nameof(model.Address));
            }

            try
            {
                var uri = $"{Config.ApiUrlBase}/clients/{clientId}/update";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Client>(false);
            }

            return new RequestResult<Client>(true);
        }
    }
}