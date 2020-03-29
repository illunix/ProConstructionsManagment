using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.AddClient
{
    public class AddClientViewModel : ViewModelBase
    {
        private readonly IClientsService _clientsService;
        private readonly IShellManager _shellManager;

        private string _clientComapanyName;
        private string _clientNIP;
        private string _clientWebsite;
        private string _clientAddress;
        private string _clientContactName;
        private string _clientContactLastName;
        private string _clientContactPhoneNumber;
        private string _clientContactEmail;

        public AddClientViewModel(IClientsService clientsService, IShellManager shellManager)
        {
            _clientsService = clientsService;
            _shellManager = shellManager;
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

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(ClientCompanyName) || string.IsNullOrWhiteSpace(ClientNIP) ||
                string.IsNullOrWhiteSpace(ClientAddress))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public ICommand AddClientCommand => new AsyncRelayCommand(AddClient);

        private async Task AddClient()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Client
                    {
                        Id = Guid.NewGuid().ToString(),
                        CompanyName = ClientNIP,
                        NIP = ClientNIP,
                        Website = ClientWebsite,
                        Address = ClientAddress,
                        ContactName = ClientContactName,
                        ContactLastName = ClientContactLastName,
                        ContactPhoneNumber = ClientContactPhoneNumber,
                        ContactEmail = ClientContactEmail
                    };

                    var result = await Task.Run(() => _clientsService.AddClient(data));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully added new client ({data.Id})");

                        MessageBox.Show("Pomyślnie dodano klienta");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding new client");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas dodawania klienta, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
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