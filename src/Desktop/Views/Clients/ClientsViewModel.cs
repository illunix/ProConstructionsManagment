using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Helpers;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Clients
{
    public class ClientsViewModel : ViewModelBase
    {
        private readonly IClientsService _clientsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _clientCount;

        private ObservableCollection<Models.Client> _clients;

        public ClientsViewModel(IClientsService clientsService, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _clientsService = clientsService;
            _shellManager = shellManager;
            _messengerService = messengerService;
        }

        public string ClientCount
        {
            get => _clientCount;
            set => Set(ref _clientCount, value);
        }

        public ObservableCollection<Models.Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        public ICommand NavigateToClientViewCommand => new AsyncRelayCommand<object>(NavigateToClientView);

        private async Task NavigateToClientView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Client));
            // _messengerService.Send(new ChangeViewMessage(ViewTypes.ClientNavigation));

            if (obj is string clientId)
            {
                _messengerService.Send(new ClientIdMessage(clientId));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                Clients = await _clientsService.GetAllClients();

                ClientCount = $"Łącznie {Clients.Count} rekordów";

                if (Clients.Count == 0)
                {
                    _messengerService.Send(new NoDataMessage(true));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading clients view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}