using ProConstructionsManagment.Desktop.Models.Base;

namespace ProConstructionsManagment.Desktop.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public bool IsForeman { get; set; }
        public bool ReadDrawings { get; set; }
        public int Status { get; set; }
    }
}