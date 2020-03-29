using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models.Base;

namespace ProConstructionsManagment.Desktop.Models
{
    public class ProjectRecruitment : BaseModel
    {
        public string ProjectId { get; set; }
        public string PositionId { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
    }
}
