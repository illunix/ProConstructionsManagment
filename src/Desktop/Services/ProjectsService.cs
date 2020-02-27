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

        public async Task<ObservableCollection<Project>> GetProjectById(Guid projectId)
        {
            var uri = $"{Config.ApiUrlBase}/projects/{projectId}";

            var json = await _requestProvider.GetAsync<RootMultiple<Project>>(uri);

            return json.Data;
        }

        public async Task<Project> AddEmployee(Project model)
        {
            var uri = $"{Config.ApiUrlBase}/project/add";

            return await _requestProvider.PostAsync(uri, model);
        }

        public async Task<Project> UpdateEmployee(Project model, Guid projectId)
        {
            var uri = $"{Config.ApiUrlBase}/projects/{projectId}/update";

            return await _requestProvider.PostAsync(uri, model);
        }
    }
}