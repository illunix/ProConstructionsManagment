using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.DTO;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Employees
{
    public class EmployeesViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;
        private readonly IMessengerService _messengerService;

        private string _employeeCount;

        private ObservableCollection<Datum> _employees;

        public EmployeesViewModel(IEmployeesService employeesService, IShellManager shellManager, IMessengerService messengerService)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
            _messengerService = messengerService;
        }

        public string EmployeeName { get; set; }

        public string EmployeeCount
        {
            get => _employeeCount;
            set => Set(ref _employeeCount, value);
        }

        public ObservableCollection<Datum> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            Employees = await _employeesService.GetAllEmployees();

            EmployeeCount = $"Łącznie {Employees.Count} rekordów";

            _shellManager.SetLoadingData(false);
        }

        public ICommand NavigateToEmployeeViewCommand => new AsyncRelayCommand<object>(NavigateToEmployeeView);

        private async Task NavigateToEmployeeView(object obj)
        {
            if (obj is string employeeId)
            {
                _messengerService.Send(new EmployeeIdMessage(employeeId));
            }

            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employee));
        }
    }
}