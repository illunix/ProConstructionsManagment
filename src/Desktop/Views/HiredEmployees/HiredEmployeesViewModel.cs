using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.HiredEmployees
{
    public class HiredEmployeesViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _hiredEmployeeCount;

        private ObservableCollection<Models.Employee> _hiredEmployees;

        public HiredEmployeesViewModel(IMessengerService messengerService, IEmployeesService employeesService, IShellManager shellManager)
        {
            _messengerService = messengerService;
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

        public ICommand NavigateToEmployeeViewCommand => new AsyncRelayCommand<object>(NavigateToEmployeeView);

        private async Task NavigateToEmployeeView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employee));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EmployeeNavigation));

            if (obj is string employeeId)
            {
                _messengerService.Send(new EmployeeIdMessage(employeeId));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                HiredEmployees = await _employeesService.GetAllHiredEmployees();

                HiredEmployeeCount = $"Łącznie {HiredEmployees.Count}";

                if (HiredEmployees.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading hired employees view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}