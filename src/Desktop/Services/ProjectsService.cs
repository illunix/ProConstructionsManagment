using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Configuration;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public class ProjectsService : IProjectsService
    {
        public readonly IRequestProvider _requestProvider;

        public ProjectsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Project>> GetAllProjects()
        {
            var uri = $"{Config.ApiUrlBase}/projects";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Project>> GetStartedProjects()
        {
            var uri = $"{Config.ApiUrlBase}/projects/started";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<int> GetStartedProjectsCount()
        {
            var uri = $"{Config.ApiUrlBase}/projects/started";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Project>> GetProjectsForStart()
        {
            var uri = $"{Config.ApiUrlBase}/projects/started";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Project>> GetProjectsForSettlement()
        {
            var uri = $"{Config.ApiUrlBase}/projects/settlement";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Project>> GetSettledProjects()
        {
            var uri = $"{Config.ApiUrlBase}/projects/settled";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Project>> GetEndedProjects()
        {
            var uri = $"{Config.ApiUrlBase}/projects/ended";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<Project> GetProjectById(string projectId)
        {
            var uri = $"{Config.ApiUrlBase}/projects/{projectId}";

            var json = await _requestProvider.GetAsync<RootSingle<Project>>(uri);

            return json.Data;
        }

        public RequestResult<Project> AddProject(Project model)
        {
            try
            {
                var uri = $"{Config.ApiUrlBase}/project/add";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Project>(false);
            }

            return new RequestResult<Project>(true);
        }

        public RequestResult<Project> UpdateProject(Project model, string projectId)
        {
            try
            {
                var uri = $"{Config.ApiUrlBase}/projects/{projectId}/update";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Project>(false);
            }

            return new RequestResult<Project>(true);
        }

        public RequestResult<ProjectCost> AddProjectCost(ProjectCost model, string projectId)
        {
            try
            {
                var uri = $"{Config.ApiUrlBase}/projects/{projectId}/costs/add";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<ProjectCost>(false);

            }

            return new RequestResult<ProjectCost>(true);

        }

        public async Task<ObservableCollection<ProjectCost>> GetProjectCosts(string projectId)
        {
            var uri = $"{Config.ApiUrlBase}/projects/{projectId}/costs";

            var json = await _requestProvider.GetAsync<RootMultiple<ProjectCost>>(uri);

            return json.Data;
        }
    }
}