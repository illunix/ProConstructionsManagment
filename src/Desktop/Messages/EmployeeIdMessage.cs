using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class EmployeeIdMessage
    {
        public EmployeeIdMessage(string employeeId)
        {
            EmployeeId = employeeId;
        }

        public string EmployeeId { get; }
    }
}
