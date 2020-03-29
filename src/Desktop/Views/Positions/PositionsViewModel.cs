using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Positions
{
    public class PositionsViewModel : ViewModelBase
    {
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _positionCount;

        private ObservableCollection<Models.Position> _positions;

        public PositionsViewModel(IPositionsService positionsService, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _positionsService = positionsService;
            _shellManager = shellManager;
            _messengerService = messengerService;
        }

        public string PositionCount
        {
            get => _positionCount;
            set => Set(ref _positionCount, value);
        }

        public ObservableCollection<Models.Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        public ICommand NavigateToPositionViewCommand => new AsyncRelayCommand<object>(NavigateToPositionView);

        private async Task NavigateToPositionView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Position));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.PositionNavigation));

            if (obj is string positionId)
            {
                _messengerService.Send(new PositionIdMessage(positionId));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                Positions = await _positionsService.GetAllPositions();

                PositionCount = $"Łącznie {Positions.Count} rekordów";

                if (Positions.Count == 0)
                {
                    _messengerService.Send(new NoDataMessage(true));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading positions view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}