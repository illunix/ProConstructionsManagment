using System.Collections.ObjectModel;

namespace ProConstructionsManagment.Desktop.Models
{
    public class RootMultiple<T> where T : class
    {
        public ObservableCollection<T> Data { get; set; }
        public Summaries Summaries { get; set; }
    }

    public class RootSingle<T> where T : class
    {
        public T Data { get; set; }
    }
}