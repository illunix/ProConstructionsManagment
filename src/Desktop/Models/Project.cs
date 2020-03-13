using ProConstructionsManagment.Desktop.Models.Base;

namespace ProConstructionsManagment.Desktop.Models
{
    public class Project : BaseModel
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PlaceOfPerformance { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
        public bool Agreement { get; set; }
        public int Status { get; set; }
    }
}