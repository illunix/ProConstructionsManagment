using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Project
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;
        private readonly IMessengerService _messengerService;

        private string _employeeId;
        private string _employeeName;
        private string _employeeSecondName;
        private string _employeeLastName;
        private string _employeeDateOfBirth;
        private string _employeeNationality;
        private bool _employeeIsForeman;
        private bool _employeeReadDrawings;

        public bool isRegistered;
        public ProjectViewModel(IEmployeesService employeesService, IShellManager shellManager, IMessengerService messengerService)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<EmployeeIdMessage>(this, EmployeeIdMessageNotify);

            GC.Collect();
        }

        public void EmployeeIdMessageNotify(EmployeeIdMessage obj)
        {
            EmployeeId = obj.EmployeeId;
        }

        public string EmployeeId
        {
            get => _employeeId;
            set => Set(ref _employeeId, value);
        }

        public string EmployeeName
        {
            get => _employeeName;
            set => Set(ref _employeeName, value);
        }

        public string EmployeeSecondName
        {
            get => _employeeSecondName;
            set => Set(ref _employeeSecondName, value);
        }

        public string EmployeeLastName
        {
            get => _employeeLastName;
            set => Set(ref _employeeLastName, value);
        }

        public string EmployeeDateOfBirth
        {
            get => _employeeDateOfBirth;
            set => Set(ref _employeeDateOfBirth, value);
        }

        public string EmployeeNationality
        {
            get => _employeeNationality;
            set => Set(ref _employeeNationality, value);
        }

        public bool EmployeeIsForeman
        {
            get => _employeeIsForeman;
            set => Set(ref _employeeIsForeman, value);
        }

        public bool EmployeeReadDrawings
        {
            get => _employeeReadDrawings;
            set => Set(ref _employeeReadDrawings, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);
                
                var employee = await _employeesService.GetEmployeeById(EmployeeId);
                
                EmployeeName = employee.Name;
                EmployeeSecondName = employee.SecondName;
                EmployeeLastName = employee.LastName;
                EmployeeDateOfBirth = employee.DateOfBirth;
                EmployeeNationality = employee.Nationality;
                EmployeeIsForeman = employee.IsForeman;
                EmployeeReadDrawings = employee.ReadDrawings;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading employee view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
        
        public ICommand UpdateEmployeeCommand => new AsyncRelayCommand(UpdateEmployee);

        private async Task UpdateEmployee()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                var data = new Models.Employee
                {
                    Name = EmployeeName,
                    DateOfBirth = EmployeeDateOfBirth,
                    LastName = EmployeeLastName,
                    IsForeman = EmployeeIsForeman,
                    ReadDrawings = EmployeeReadDrawings
                };
                
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed updating employee");

                MessageBox.Show("Coś poszło nie tak podczas dodawania pracownika, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}