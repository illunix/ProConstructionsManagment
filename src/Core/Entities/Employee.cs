using System;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Infrastructure.Data.Entities.Base;

namespace ProConstructionsManagment.Infrastructure.Data.Entities
{
    public class Employee : BaseEntity
    {
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