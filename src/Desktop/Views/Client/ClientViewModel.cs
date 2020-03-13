using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Client
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly IClientsService _clientsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _clientId;
        private string _clientComapanyName;
        private string _clientNIP;
        private string _clientWebsite;
        private string _clientAddress;
        private string _clientContactName;
        private string _clientContactLastName;
        private string _clientContactPhoneNumber;
        private string _clientContactEmail;

        public ClientViewModel(IClientsService clientsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _clientsService = clientsService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<ClientIdMessage>(this, msg => ClientId = msg.ClientId);
        }

        public string ClientId
        {
            get => _clientId;
            set => Set(ref _clientId, value);
        }

        public string ClientCompanyName
        {
            get => _clientComapanyName;
            set => Set(ref _clientComapanyName, value);
        }

        public string ClientNIP
        {
            get => _clientNIP;
            set => Set(ref _clientNIP, value);
        }

        public string ClientWebsite
        {
            get => _clientWebsite;
            set => Set(ref _clientWebsite, value);
        }

        public string ClientAddress
        {
            get => _clientAddress;
            set => Set(ref _clientAddress, value);
        }

        public string ClientContactName
        {
            get => _clientContactName;
            set => Set(ref _clientContactName, value);
        }

        public string ClientContactLastName
        {
            get => _clientContactLastName;
            set => Set(ref _clientContactLastName, value);
        }

        public string ClientContactPhoneNumber
        {
            get => _clientContactPhoneNumber;
            set => Set(ref _clientContactPhoneNumber, value);
        }

        public string ClientContactEmail
        {
            get => _clientContactEmail;
            set => Set(ref _clientContactEmail, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                var client = await _clientsService.GetClientById(ClientId);

                ClientCompanyName = client.CompanyName;
                ClientNIP = client.NIP;
                ClientWebsite = client.Website;
                ClientAddress = client.Address;
                ClientContactName = client.ContactName;
                ClientContactLastName = client.ContactLastName;
                ClientContactEmail = client.ContactEmail;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading client view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(ClientCompanyName) || string.IsNullOrWhiteSpace(ClientNIP) ||
                string.IsNullOrWhiteSpace(ClientAddress))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public ICommand UpdateClientCommand => new AsyncRelayCommand(UpdateClient);

        private async Task UpdateClient()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var client = await _clientsService.GetClientById(ClientId);

                    var data = new Models.Client
                    {
                        Id = ClientId,
                        CompanyName = ClientNIP,
                        NIP = ClientNIP,
                        Website = ClientWebsite,
                        Address = ClientAddress,
                        ContactName = ClientContactName,
                        ContactLastName = ClientContactLastName,
                        ContactPhoneNumber = ClientContactPhoneNumber,
                        ContactEmail = ClientContactEmail
                    };

                    var result = await Task.Run(() => _clientsService.UpdateClient(data, ClientId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully updated client ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed updating client");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
                }
                finally
                {
                    _shellManager.SetLoadingData(false);
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola");
            }
        }
    }
}
