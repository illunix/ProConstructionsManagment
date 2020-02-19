using System.Collections.ObjectModel;
using ProConstructionsManagment.Desktop.Models.Base;

namespace ProConstructionsManagment.Desktop.Models
{
    public class Project : BaseModel
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PlaceOfPerformance { get; set; }
        public int PercentageOfImplementation { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfCandidates { get; set; }
        public int NumberOfHours { get; set; }
        public bool Agreement { get; set; }
        public int Status { get; set; }
    }
}