using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Configuration;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public class PositionsService : IPositionsService
    {
        private readonly IRequestProvider _requestProvider;

        public PositionsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Position>> GetAllPositions()
        {
            var uri = $"{Config.ApiUrlBase}/positions";

            var json = await _requestProvider.GetAsync<RootMultiple<Position>>(uri);

            return json.Data;
        }

        public async Task<Position> GetPositionById(string positionId)
        {
            var uri = $"{Config.ApiUrlBase}/positions/{positionId}";

            var json = await _requestProvider.GetAsync<RootSingle<Position>>(uri);

            return json.Data;
        }

        public RequestResult<Position> AddPosition(Position model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (string.IsNullOrWhiteSpace(model.JobDescription))
            {
                throw new ArgumentNullException(nameof(model.JobDescription));
            }

            try
            {
                var uri = $"{Config.ApiUrlBase}/position/add";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Position>(false);
            }

            return new RequestResult<Position>(true);
        }

        public RequestResult<Position> UpdatePosition(Position model, string positionId)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (string.IsNullOrWhiteSpace(model.JobDescription))
            {
                throw new ArgumentNullException(nameof(model.JobDescription));
            }

            try
            {
                var uri = $"{Config.ApiUrlBase}/positions/{positionId}/update";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Position>(false);
            }

            return new RequestResult<Position>(true);
        }

        public RequestResult<Position> RemovePosition(string positionId)
        {
            try
            {
                var uri = $"{Config.ApiUrlBase}/positions/{positionId}/remove";

                _requestProvider.DeleteAsync(uri);
            }
            catch
            {
                return new RequestResult<Position>(false);
            }

            return new RequestResult<Position>(true);
        }
    }
}
