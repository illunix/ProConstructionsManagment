using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Employees
{
    public class EmployeesNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public EmployeesNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;

            messengerService.Register<CurrentViewTypeMessage>(this, CurrentViewTypeMessageNotify);
        }

        private ViewTypes CurrentViewType { get; set; }

        private void CurrentViewTypeMessageNotify(CurrentViewTypeMessage obj)
        {
            CurrentViewType = obj.ViewType;
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToEmployeesViewCommand => new AsyncRelayCommand(NavigateToEmployeesView);

        private async Task NavigateToEmployeesView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employees));
        }

        public ICommand NavigateToHiredEmployeesViewCommand => new AsyncRelayCommand(NavigateToHiredEmployeesView);

        private async Task NavigateToHiredEmployeesView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.HiredEmployees));
        }

        public ICommand NavigateToEmployeesForHireViewCommand => new AsyncRelayCommand(NavigateToEmployeesForHireView);

        private async Task NavigateToEmployeesForHireView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EmployeesForHire));
        }

        public ICommand NavigateToAddEmployeeViewCommand => new AsyncRelayCommand(NavigateToAddEmployeeView);

        private async Task NavigateToAddEmployeeView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddEmployee));
        }

        /*
        public ICommand ReloadEmployeesViewCommand => new AsyncRelayCommand(ReloadEmployeesView);

        private async Task ReloadEmployeesView()
        {
            var hiredViewModel = ViewModelLocator.Get<HiredEmployeesViewModel>();

            if (CurrentViewType == ViewTypes.HiredEmployees)
            {
                _messengerService.Send(new ChangeViewMessage(ViewTypes.HiredEmployees));
            }

            var viewModel = ViewModelLocator.Get<EmployeesViewModel>();

            await viewModel.Initialize();
        }
        */

        /*
        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }
        */
    }
}