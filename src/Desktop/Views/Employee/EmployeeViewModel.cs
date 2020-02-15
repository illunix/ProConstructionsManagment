using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProConstructionsManagment.Desktop.Views.Employee
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;

        public EmployeeViewModel(IEmployeesService employeesService, IMessengerService messengerService)
        {
            _employeesService = employeesService;
            _messengerService = messengerService;

            messengerService.Register<EmployeeIdMessage>(this, EmployeeMessageNotify);
        }

        public string EmployeeId { get; set; }

        private void EmployeeMessageNotify(EmployeeIdMessage obj)
        {
            EmployeeId = obj.EmployeeId;
        }

        public async Task Initialize()
        {
            MessageBox.Show(EmployeeId);
        }
    }
}
