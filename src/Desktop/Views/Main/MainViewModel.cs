using System;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private int _employeeCount;
        private int _startedProjectsCount;

        public MainViewModel(IEmployeesService employeesService, IProjectsService projectsService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _projectsService = projectsService;
            _shellManager = shellManager;
        }
        
        public int EmployeeCount
        {
            get => _employeeCount;
            set => Set(ref _employeeCount, value);
        }

        public int StartedProjectsCount
        {
            get => _startedProjectsCount;
            set => Set(ref _startedProjectsCount, value);
        }

        public async Task InitializeAsync()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                EmployeeCount = await _employeesService.GetAllEmployeesCount();

                StartedProjectsCount = await _projectsService.GetStartedProjectsCount();
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading main view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
        
            }
        }
    }
}