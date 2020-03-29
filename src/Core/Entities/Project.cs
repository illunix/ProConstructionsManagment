using System;
using ProConstructionsManagment.Core.Entities.Base;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Core.Entities
{
    public class Project : BaseEntity, IEnumEntity<ProjectStatus>
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PlaceOfPerformance { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
        public bool Agreement { get; set; }
        public ProjectStatus Status { get; set; }
    }
}