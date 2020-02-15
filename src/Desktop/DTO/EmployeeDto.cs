using System.Collections.ObjectModel;

namespace ProConstructionsManagment.Desktop.DTO
{
    public class Root
    {
        public ObservableCollection<Datum> data { get; set; }
        public Summaries Summaries { get; set; }
    }

    public class Datum
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public bool IsForeman { get; set; }
        public bool ReadDrawings { get; set; }
        public int Status { get; set; }
    }

    public class Summaries
    {
        public int Count { get; set; }
    }
}