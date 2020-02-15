using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.AddEmployee;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Employee;
using ProConstructionsManagment.Desktop.Views.Employees;
using ProConstructionsManagment.Desktop.Views.EmployeesForHire;
using ProConstructionsManagment.Desktop.Views.HiredEmployees;
using ProConstructionsManagment.Desktop.Views.Main;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Views.Shell
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        private readonly IViewModelLocator _viewModelLocator;
        private ViewModelBase _currentNavigationViewModel;

        private ViewModelBase _currentViewModel;

        private bool _isLoadingData;

        public ShellViewModel(IViewModelLocator viewModelLocator, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _viewModelLocator = viewModelLocator;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<ChangeViewMessage>(this, ChangeViewMessageNotify);
            messengerService.Register<LoadingDataMessage>(this, msg => IsLoadingData = msg.IsLoadingData);
        }

        public bool IsLoadingData
        {
            get => _isLoadingData;
            set => Set(ref _isLoadingData, value);
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Cleanup();
                Set(ref _currentViewModel, value);
            }
        }

        public ViewModelBase CurrentNavigationViewModel
        {
            get => _currentNavigationViewModel;
            set
            {
                _currentNavigationViewModel?.Cleanup();
                Set(ref _currentNavigationViewModel, value);
            }
        }

        private void ChangeViewMessageNotify(ChangeViewMessage obj)
        {
            switch (obj.View)
            {
                case ViewTypes.Main:
                    var mainViewModel = _viewModelLocator.Get<MainViewModel>();
                    CurrentViewModel = mainViewModel;
                    break;
                case ViewTypes.MainNavigation:
                    var mainNavigationViewModel = _viewModelLocator.Get<MainNavigationViewModel>();
                    CurrentNavigationViewModel = mainNavigationViewModel;
                    break;
                case ViewTypes.Employee:
                    var employeeViewModel = _viewModelLocator.Get<EmployeeViewModel>();
                    CurrentViewModel = employeeViewModel;
                    break;
                case ViewTypes.Employees:
                    var employeesViewModel = _viewModelLocator.Get<EmployeesViewModel>();
                    CurrentViewModel = employeesViewModel;
                    // _messengerService.Send(new CurrentViewModelMessage(CurrentViewModel));
                    break;
                case ViewTypes.EmployeesNavigation:
                    var employeesNavigationViewModel = _viewModelLocator.Get<EmployeesNavigationViewModel>();
                    CurrentNavigationViewModel = employeesNavigationViewModel;
                    break;

                case ViewTypes.EmployeesForHire:
                    var employeesForHireViewModel = _viewModelLocator.Get<EmployeesForHireViewModel>();
                    CurrentViewModel = employeesForHireViewModel;
                    break;

                case ViewTypes.HiredEmployees:
                    var hiredEmployeesViewModel = _viewModelLocator.Get<HiredEmployeesViewModel>();
                    CurrentViewModel = hiredEmployeesViewModel;
                    // _messengerService.Send(new CurrentViewTypeMessage(ViewTypes.HiredEmployees));
                    break;
                case ViewTypes.AddEmployee:
                    _shellManager.SetLoadingData(true);
                    var addEmployeeViewModel = _viewModelLocator.Get<AddEmployeeViewModel>();
                    CurrentViewModel = addEmployeeViewModel;
                    _shellManager.SetLoadingData(false);
                    break;
            }
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            var mainViewModel = _viewModelLocator.Get<MainViewModel>();

            CurrentViewModel = mainViewModel;

            _shellManager.SetLoadingData(false);

            var mainNavigationViewModel = _viewModelLocator.Get<MainNavigationViewModel>();

            CurrentNavigationViewModel = mainNavigationViewModel;
        }
    }
}