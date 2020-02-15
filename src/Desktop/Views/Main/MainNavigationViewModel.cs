using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public class MainNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public MainNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand EmployeesCommand => new AsyncRelayCommand(NavigateToEmployeesView);

        private async Task NavigateToEmployeesView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employees));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EmployeesNavigation));
        }
    }
}