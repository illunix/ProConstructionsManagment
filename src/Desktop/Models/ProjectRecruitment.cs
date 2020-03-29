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