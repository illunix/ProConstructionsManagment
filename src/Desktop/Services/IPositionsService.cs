using ProConstructionsManagment.Desktop.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IPositionsService
    {
        Task<ObservableCollection<Position>> GetAllPositions();

        Task<Position> GetPositionById(string clientId);

        RequestResult<Position> AddPosition(Position model);

        RequestResult<Position> UpdatePosition(Position model, string positionId);

        RequestResult<Position> RemovePosition(string positionId);
    }
}