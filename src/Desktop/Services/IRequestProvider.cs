using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");
        Task DeleteAsync(string uri, string token = "");
    }
}