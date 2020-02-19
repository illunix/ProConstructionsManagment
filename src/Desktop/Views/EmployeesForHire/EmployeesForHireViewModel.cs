using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Messages;

namespace ProConstructionsManagment.Desktop.Views.EmployeesForHire
{
    public class EmployeesForHireViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _employeeForHireCount;

        private ObservableCollection<Models.Employee> _employeesForHire;

        public EmployeesForHireViewModel(IEmployeesService employeesService, IMessengerService messengerService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string EmployeeForHireCount
        {
            get => _employeeForHireCount;
            set => Set(ref _employeeForHireCount, value);
        }

        public ObservableCollection<Models.Employee> EmployeesForHire
        {
            get => _employeesForHire;
            set => Set(ref _employeesForHire, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            EmployeesForHire = await _employeesService.GetAllEmployeesForHire();

            EmployeeForHireCount = $"Łącznie {EmployeesForHire.Count} rekordów";

            if (EmployeesForHire.Count == 0)
            {
                _messengerService.Send(new NoDataMessage(true));
            }

            _shellManager.SetLoadingData(false);
        }
    }
}