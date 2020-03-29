using System;
using ProConstructionsManagment.Core.Entities.Base;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Core.Entities
{
    public class Employee : BaseEntity, IEnumEntity<EmployeeStatus>
    {
        public Guid ProjectId { get; set; }
        public Guid PositionId { get; set; }
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