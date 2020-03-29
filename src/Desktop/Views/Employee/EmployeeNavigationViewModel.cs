using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Employee
{
    public class EmployeeNavigationViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _employeeId;

        public EmployeeNavigationViewModel(IEmployeesService employeesService, IMessengerService messengerService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<EmployeeIdMessage>(this, msg => EmployeeId = msg.EmployeeId);
        }

        public string EmployeeId
        {
            get => _employeeId;
            set => Set(ref _employeeId, value);
        }

        public ICommand NavigateToEmployeesViewCommand => new AsyncRelayCommand(NavigateToEmployeesView);

        private async Task NavigateToEmployeesView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employees));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EmployeesNavigation));
        }

        public ICommand HireEmployeeCommand => new AsyncRelayCommand(HireEmployee);

        private async Task HireEmployee()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Czy jesteś pewien że chcesz zatrudnić tego pracownika?", "", MessageBoxButton.YesNo);

                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        _shellManager.SetLoadingData(true);

                        var employee = await _employeesService.GetEmployeeById(EmployeeId);

                        var data = new Models.Employee
                        {
                            Id = EmployeeId,
                            Name = employee.Name,
                            SecondName = employee.SecondName,
                            LastName = employee.LastName,
                            DateOfBirth = employee.DateOfBirth,
                            Nationality = employee.Nationality,
                            IsForeman = employee.IsForeman,
                            ReadDrawings = employee.ReadDrawings,
                            Status = 1
                        };

                        var result = _employeesService.UpdateEmployee(data, EmployeeId);
                        if (result.IsSuccessful)
                        {
                            Log.Information($"Successfully changed employee status to hired ({data.Id})");

                            MessageBox.Show("Pomyślnie zatrudniono pracownika");
                        }
                        break;

                    case MessageBoxResult.No:
                        return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed updating employee");

                MessageBox.Show(
                    "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}