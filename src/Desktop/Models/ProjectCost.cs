using ProConstructionsManagment.Desktop.Models.Base;
using System;

namespace ProConstructionsManagment.Desktop.Models
{
    public class ProjectCost : BaseModel
    {
        public Guid ProjectId { get; set; }
        public int GrossAmount { get; set; }
        public string CostDescription { get; set; }
    }
}