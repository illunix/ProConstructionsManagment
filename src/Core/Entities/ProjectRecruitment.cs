using System;
using System.Collections.Generic;
using System.Text;
using ProConstructionsManagment.Core.Entities.Base;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Core.Entities
{
    public class ProjectRecruitment : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid PositionId { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
    }
}
