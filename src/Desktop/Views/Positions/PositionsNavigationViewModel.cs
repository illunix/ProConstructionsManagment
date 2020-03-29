using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Positions
{
    public class PositionsNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public PositionsNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToPositionsViewCommand => new AsyncRelayCommand(NavigateToPositionsView);

        private async Task NavigateToPositionsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Positions));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.PositionsNavigation));
        }

        public ICommand NavigateToAddPositionViewCommand => new AsyncRelayCommand(NavigateToAddPositionView);

        private async Task NavigateToAddPositionView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddPosition));
        }
    }
}
