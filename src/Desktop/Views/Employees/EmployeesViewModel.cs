﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Employees
{
    public class EmployeesViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _employeeCount;

        private ObservableCollection<Models.Employee> _employees;

        public EmployeesViewModel(IEmployeesService employeesService, IShellManager shellManager,
            IMessengerService messengerService)
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

        public ObservableCollection<Models.Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        public ICommand NavigateToEmployeeViewCommand => new AsyncRelayCommand<object>(NavigateToEmployeeView);

        private async Task NavigateToEmployeeView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employee));
            
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
                
                Employees = await _employeesService.GetAllEmployees();

                EmployeeCount = $"Łącznie {Employees.Count} rekordów";
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading employees view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}