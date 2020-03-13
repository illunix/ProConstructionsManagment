using System;
using System.Collections.Generic;
using System.Text;
using ProConstructionsManagment.Desktop.Models.Base;

namespace ProConstructionsManagment.Desktop.Models
{
    public class ProjectCost : BaseModel
    {
        public Guid ProjectId { get; set; }
        public int GrossAmount { get; set; }
        public string CostDescription { get; set; }
    }
}
