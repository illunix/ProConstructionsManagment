using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.AddClient;
using ProConstructionsManagment.Desktop.Views.AddEmployee;
using ProConstructionsManagment.Desktop.Views.AddEmployeesToProjectRecruitment;
using ProConstructionsManagment.Desktop.Views.AddPosition;
using ProConstructionsManagment.Desktop.Views.AddProject;
using ProConstructionsManagment.Desktop.Views.AddProjectCost;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Client;
using ProConstructionsManagment.Desktop.Views.Clients;
using ProConstructionsManagment.Desktop.Views.Employee;
using ProConstructionsManagment.Desktop.Views.Employees;
using ProConstructionsManagment.Desktop.Views.EmployeesForHire;
using ProConstructionsManagment.Desktop.Views.EndedProjects;
using ProConstructionsManagment.Desktop.Views.HiredEmployees;
using ProConstructionsManagment.Desktop.Views.Main;
using ProConstructionsManagment.Desktop.Views.OpenedProjects;
using ProConstructionsManagment.Desktop.Views.Position;
using ProConstructionsManagment.Desktop.Views.Positions;
using ProConstructionsManagment.Desktop.Views.Project;
using ProConstructionsManagment.Desktop.Views.ProjectCosts;
using ProConstructionsManagment.Desktop.Views.AddProjectRecruitment;
using ProConstructionsManagment.Desktop.Views.ProjectRecruitment;
using ProConstructionsManagment.Desktop.Views.ProjectRecruitments;
using ProConstructionsManagment.Desktop.Views.Projects;
using ProConstructionsManagment.Desktop.Views.ProjectSettlements;
using ProConstructionsManagment.Desktop.Views.ProjectsToStart;
using ProConstructionsManagment.Desktop.Views.RecruitedEmployeesWithPosition;

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

        private ViewModelBase _previousViewModel;

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

        public ViewModelBase PreviousViewModel
        {
            get => _previousViewModel;
            set => Set(ref _previousViewModel, value);
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _previousViewModel = _currentViewModel;
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
                    // NoData = false;
                    CurrentViewModel = employeesViewModel;
                    // _messengerService.Send(new CurrentViewModelMessage(CurrentViewModel));
                    break;
                case ViewTypes.EmployeeNavigation:
                    var employeeNavigationViewModel = _viewModelLocator.Get<Employee.EmployeeNavigationViewModel>();
                    CurrentNavigationViewModel = employeeNavigationViewModel;
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
                case ViewTypes.Project:
                    var projectViewModel = _viewModelLocator.Get<ProjectViewModel>();
                    NoData = false;
                    CurrentViewModel = projectViewModel;
                    break;
                case ViewTypes.ProjectCosts:
                    var projectCostsViewModel = _viewModelLocator.Get<ProjectCostsViewModel>();
                    CurrentViewModel = projectCostsViewModel;
                    break;
                case ViewTypes.Projects:
                    var projectsViewModel = _viewModelLocator.Get<ProjectsViewModel>();
                    NoData = false;
                    CurrentViewModel = projectsViewModel;
                    break;
                case ViewTypes.ProjectNavigation:
                    var projectNavigationViewModel = _viewModelLocator.Get<ProjectNavigationViewModel>();
                    CurrentNavigationViewModel = projectNavigationViewModel;
                    break;
                case ViewTypes.ProjectCostsNavigation:
                    var projectCostsNavigationViewModel = _viewModelLocator.Get<ProjectCostsNavigationViewModel>();
                    CurrentNavigationViewModel = projectCostsNavigationViewModel;
                    break;
                case ViewTypes.ProjectsNavigation:
                    var projectsNavigationViewModel = _viewModelLocator.Get<ProjectsNavigationViewModel>();
                    CurrentNavigationViewModel = projectsNavigationViewModel;
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
                case ViewTypes.ProjectRecruitment:
                    var projectRecruitmentViewModel = _viewModelLocator.Get<ProjectRecruitmentViewModel>();
                    CurrentViewModel = projectRecruitmentViewModel;
                    break;
                case ViewTypes.ProjectRecruitmentNavigation:
                    var projectRecruitmentNavigationViewModel = _viewModelLocator.Get<ProjectRecruitmentNavigationViewModel>();
                    CurrentNavigationViewModel = projectRecruitmentNavigationViewModel;
                    break;
                case ViewTypes.ProjectRecruitments:
                    var projectRecruitmentsViewModel = _viewModelLocator.Get<ProjectRecruitmentsViewModel>();
                    CurrentViewModel = projectRecruitmentsViewModel;
                    break;
                case ViewTypes.ProjectRecruitmentsNavigation:
                    var projectRecruitmentsNavigationViewModel = _viewModelLocator.Get<ProjectRecruitmentsNavigationViewModel>();
                    CurrentNavigationViewModel = projectRecruitmentsNavigationViewModel;
                    break;
                case ViewTypes.AddProjectRecruitment:
                    var addProjectRecruitmentViewModel = _viewModelLocator.Get<AddProjectRecruitmentViewModel>();
                    CurrentViewModel = addProjectRecruitmentViewModel;
                    break;
                case ViewTypes.AddProjectRecruitmentNavigation:
                    var addProjectRecruitmentNavigationViewModel = _viewModelLocator.Get<AddProjectRecruitmentNavigationViewModel>();
                    CurrentNavigationViewModel = addProjectRecruitmentNavigationViewModel;
                    break;
                case ViewTypes.AddEmployeesToProjectRecruitment:
                    var addEmployeesToProjectRecruitmentViewModel = _viewModelLocator.Get<AddEmployeesToProjectRecruitmentViewModel>();
                    CurrentViewModel = addEmployeesToProjectRecruitmentViewModel;
                    break;
                case ViewTypes.AddEmployeesToProjectRecruitmentNavigation:
                    var addEmployeesToProjectRecruitmentNavigationViewModel = _viewModelLocator.Get<AddEmployeesToProjectRecruitmentNavigationViewModel>();
                    CurrentNavigationViewModel = addEmployeesToProjectRecruitmentNavigationViewModel;
                    break;
                case ViewTypes.RecruitedEmployeesWithPosition:
                    var recruitedEmployeesWithPositionViewModel = _viewModelLocator.Get<RecruitedEmployeesWithPositionViewModel>();
                    CurrentViewModel = recruitedEmployeesWithPositionViewModel;
                    break;
                case ViewTypes.EndedProjects:
                    var endedProjectsViewModel = _viewModelLocator.Get<EndedProjectsViewModel>();
                    NoData = false;
                    CurrentViewModel = endedProjectsViewModel;
                    break;
                case ViewTypes.AddProject:
                    var addProjectViewModel = _viewModelLocator.Get<AddProjectViewModel>();
                    NoData = false;
                    CurrentViewModel = addProjectViewModel;
                    break;
                case ViewTypes.AddProjectCost:
                    var addProjectCostViewModel = _viewModelLocator.Get<AddProjectCostViewModel>();
                    CurrentViewModel = addProjectCostViewModel;
                    break;
                case ViewTypes.Client:
                    var clientViewModel = _viewModelLocator.Get<ClientViewModel>();
                    CurrentViewModel = clientViewModel;
                    break;
                case ViewTypes.ClientNavigation:
                    var clientNavigationViewModel = _viewModelLocator.Get<ClientNavigationViewModel>();
                    CurrentNavigationViewModel = clientNavigationViewModel;
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
                case ViewTypes.AddClient:
                    var addClientViewModel = _viewModelLocator.Get<AddClientViewModel>();
                    CurrentViewModel = addClientViewModel;
                    break;
                case ViewTypes.AddProjectCostNavigation:
                    var addProjectCostNavigation = _viewModelLocator.Get<AddProjectCostNavigationViewModel>();
                    CurrentNavigationViewModel = addProjectCostNavigation;
                    break;
                case ViewTypes.Position:
                    var positionViewModel = _viewModelLocator.Get<PositionViewModel>();
                    CurrentViewModel = positionViewModel;
                    break;
                case ViewTypes.PositionNavigation:
                    var positionNavigationViewModel = _viewModelLocator.Get<PositionNavigationViewModel>();
                    CurrentNavigationViewModel = positionNavigationViewModel;
                    break;
                case ViewTypes.Positions:
                    var positionsViewModel = _viewModelLocator.Get<PositionsViewModel>();
                    CurrentViewModel = positionsViewModel;
                    break;
                case ViewTypes.PositionsNavigation:
                    var positionsNavigationViewModel = _viewModelLocator.Get<PositionsNavigationViewModel>();
                    CurrentNavigationViewModel = positionsNavigationViewModel;
                    break;
                case ViewTypes.AddPosition:
                    var addPositionViewModel = _viewModelLocator.Get<AddPositionViewModel>();
                    CurrentViewModel = addPositionViewModel;
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