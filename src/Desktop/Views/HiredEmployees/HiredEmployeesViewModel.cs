using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.HiredEmployees
{
    public class HiredEmployeesViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _hiredEmployeeCount;

        private ObservableCollection<Models.Employee> _hiredEmployees;

        public HiredEmployeesViewModel(IEmployeesService employeesService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
        }

        public string HiredEmployeeCount
        {
            get => _hiredEmployeeCount;
            set => Set(ref _hiredEmployeeCount, value);
        }

        public ObservableCollection<Models.Employee> HiredEmployees
        {
            get => _hiredEmployees;
            set => Set(ref _hiredEmployees, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            HiredEmployees = await _employeesService.GetAllHiredEmployees();

            HiredEmployeeCount = $"Łącznie {HiredEmployees.Count}";

            if (HiredEmployees.Count == 0)
            {
                _messengerService.Send(new NoDataMessage(true));
            }

            _shellManager.SetLoadingData(false);
        }
    }
}