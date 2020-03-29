using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models;

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
