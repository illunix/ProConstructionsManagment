using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ProConstructionsManagment.Desktop.Models
{
    public class Root<T> where T: class
    {
        public ObservableCollection<T> Data { get; set; }
        public Summaries Summaries { get; set; }
    }
}