using ProConstructionsManagment.Infrastructure.Enums;
using System;

namespace ProConstructionsManagment.Infrastructure.Data.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public bool IsForeman { get; set; }
        public bool ReadDrawings { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}