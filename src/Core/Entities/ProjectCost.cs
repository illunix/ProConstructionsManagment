using ProConstructionsManagment.Core.Entities.Base;
using System;

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