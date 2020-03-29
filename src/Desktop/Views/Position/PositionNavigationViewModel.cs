using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Position
{
    public class PositionNavigationViewModel : ViewModelBase
    {
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _positionId;

        public PositionNavigationViewModel(IPositionsService positionsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _positionsService = positionsService;
            _messengerService = messengerService;
            _shellManager = shellManager;   

            messengerService.Register<PositionIdMessage>(this, msg => PositionId = msg.PositionId);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        public ICommand NavigateToPositionsViewCommand => new AsyncRelayCommand(NavigateToPositionsView);

        private async Task NavigateToPositionsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Positions));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.PositionsNavigation));
        }

        public ICommand RemovePositionCommand => new AsyncRelayCommand(RemovePosition);

        private async Task RemovePosition()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Czy jesteś pewien/pewna że chcesz usunąć te stanowisko?", "", MessageBoxButton.YesNo);

                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        _shellManager.SetLoadingData(true);

                        var result = _positionsService.RemovePosition(PositionId);
                        if (result.IsSuccessful)
                        {
                            Log.Information($"Successfully removed position ({PositionId})");

                            MessageBox.Show("Pomyślnie usunięto stanowisko");

                            _messengerService.Send(new ChangeViewMessage(ViewTypes.Positions));
                            _messengerService.Send(new ChangeViewMessage(ViewTypes.PositionsNavigation));
                            _shellManager.SetLoadingData(false);
                        }
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed deleting position");

                MessageBox.Show(
                    "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}
