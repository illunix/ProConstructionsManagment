using ProConstructionsManagment.Core.Entities.Base;
using System;

namespace ProConstructionsManagment.Core.Entities
{
    public class ProjectRecruitment : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid PositionId { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
    }
}