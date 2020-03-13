using System;
using System.Collections.Generic;
using System.Text;
using ProConstructionsManagment.Core.Entities.Base;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Core.Entities
{
    public class ProjectCost : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public int GrossAmount { get; set; }
        public string CostDescription { get; set; }
    }
}
