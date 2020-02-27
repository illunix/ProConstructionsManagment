using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.AddEmployee;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Clients;
using ProConstructionsManagment.Desktop.Views.Employee;
using ProConstructionsManagment.Desktop.Views.Employees;
using ProConstructionsManagment.Desktop.Views.EmployeesForHire;
using ProConstructionsManagment.Desktop.Views.EndedProjects;
using ProConstructionsManagment.Desktop.Views.HiredEmployees;
using ProConstructionsManagment.Desktop.Views.Main;
using ProConstructionsManagment.Desktop.Views.OpenedProjects;
using ProConstructionsManagment.Desktop.Views.Projects;
using ProConstructionsManagment.Desktop.Views.ProjectSettlements;
using ProConstructionsManagment.Desktop.Views.ProjectsToStart;

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

        private bool _noData;

        public ShellViewModel(IViewModelLocator viewModelLocator, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _viewModelLocator = viewModelLocator;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<ChangeViewMessage>(this, ChangeViewMessageNotify);
            messengerService.Register<NoDataMessage>(this, msg => NoData = msg.NoData);
            messengerService.Register<LoadingDataMessage>(this, msg => IsLoadingData = msg.IsLoadingData);
        }

        public bool NoData
        {
            get => _noData;
            set => Set(ref _noData, value);
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
                    NoData = false;
                    CurrentViewModel = mainViewModel;
                    break;
                case ViewTypes.MainNavigation:
                    var mainNavigationViewModel = _viewModelLocator.Get<MainNavigationViewModel>();
                    CurrentNavigationViewModel = mainNavigationViewModel;
                    break;
                case ViewTypes.Employee:
                    var employeeViewModel = _viewModelLocator.Get<EmployeeViewModel>();
                    NoData = false;
                    CurrentViewModel = employeeViewModel;
                    break;
                case ViewTypes.Employees:
                    var employeesViewModel = _viewModelLocator.Get<EmployeesViewModel>();
                    NoData = false;
                    CurrentViewModel = employeesViewModel;
                    // _messengerService.Send(new CurrentViewModelMessage(CurrentViewModel));
                    break;
                case ViewTypes.EmployeesNavigation:
                    var employeesNavigationViewModel = _viewModelLocator.Get<EmployeesNavigationViewModel>();
                    CurrentNavigationViewModel = employeesNavigationViewModel;
                    break;

                case ViewTypes.EmployeesForHire:
                    var employeesForHireViewModel = _viewModelLocator.Get<EmployeesForHireViewModel>();
                    NoData = false;
                    CurrentViewModel = employeesForHireViewModel;
                    break;
                case ViewTypes.HiredEmployees:
                    var hiredEmployeesViewModel = _viewModelLocator.Get<HiredEmployeesViewModel>();
                    NoData = false;
                    CurrentViewModel = hiredEmployeesViewModel;
                    // _messengerService.Send(new CurrentViewTypeMessage(ViewTypes.HiredEmployees));
                    break;
                case ViewTypes.AddEmployee:
                    _shellManager.SetLoadingData(true);
                    var addEmployeeViewModel = _viewModelLocator.Get<AddEmployeeViewModel>();
                    NoData = false;
                    CurrentViewModel = addEmployeeViewModel;
                    _shellManager.SetLoadingData(false);
                    break;
                case ViewTypes.Projects:
                    var projectViewModel = _viewModelLocator.Get<ProjectsViewModel>();
                    NoData = false;
                    CurrentViewModel = projectViewModel;
                    break;
                case ViewTypes.ProjectsNavigation:
                    var projectNavigationViewModel = _viewModelLocator.Get<ProjectsNavigationViewModel>();
                    CurrentNavigationViewModel = projectNavigationViewModel;
                    break;
                case ViewTypes.OpenedProjects:
                    var openedProjectsViewModel = _viewModelLocator.Get<OpenedProjectsViewModel>();
                    NoData = false;
                    CurrentViewModel = openedProjectsViewModel;
                    break;
                case ViewTypes.ProjectsToStart:
                    var projectsToStartViewModel = _viewModelLocator.Get<ProjectsToStartViewModel>();
                    NoData = false;
                    CurrentViewModel = projectsToStartViewModel;
                    break;
                case ViewTypes.ProjectSettlements:
                    var projectSettlementsViewModel = _viewModelLocator.Get<ProjectSettlementsViewModel>();
                    NoData = false;
                    CurrentViewModel = projectSettlementsViewModel;
                    break;
                case ViewTypes.EndedProjects:
                    var endedProjectsViewModel = _viewModelLocator.Get<EndedProjectsViewModel>();
                    NoData = false;
                    CurrentViewModel = endedProjectsViewModel;
                    break;
                case ViewTypes.Clients:
                    var clientsViewModel = _viewModelLocator.Get<ClientsViewModel>();
                    NoData = false;
                    CurrentViewModel = clientsViewModel;
                    break;
                case ViewTypes.ClientsNavigation:
                    var clientsNavigationViewModel = _viewModelLocator.Get<ClientsNavigationViewModel>();
                    NoData = false;
                    CurrentNavigationViewModel = clientsNavigationViewModel;
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