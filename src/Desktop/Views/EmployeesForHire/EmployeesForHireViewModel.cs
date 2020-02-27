using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.EmployeesForHire
{
    public class EmployeesForHireViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _employeeForHireCount;

        private ObservableCollection<Models.Employee> _employeesForHire;

        public EmployeesForHireViewModel(IEmployeesService employeesService, IMessengerService messengerService,
            IShellManager shellManager)
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
            try
            {
                _shellManager.SetLoadingData(true);

                EmployeesForHire = await _employeesService.GetAllEmployeesForHire();

                EmployeeForHireCount = $"Łącznie {EmployeesForHire.Count} rekordów";

                if (EmployeesForHire.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading employees for hire view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}