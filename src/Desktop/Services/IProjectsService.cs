using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IProjectsService
    {
        Task<ObservableCollection<Project>> GetAllProjects();

        Task<ObservableCollection<Project>> GetStartedProjects();

        Task<int> GetStartedProjectsCount();
        
        Task<ObservableCollection<Project>> GetProjectsForStart();

        Task<ObservableCollection<Project>> GetProjectsForSettlement();

        Task<ObservableCollection<Project>> GetSettledProjects();

        Task<ObservableCollection<Project>> GetEndedProjects();

        Task<Project> GetProjectById(string projectId);

        RequestResult<Project> AddProject(Project model);

        RequestResult<Project> UpdateProject(Project model, string projectId);

        RequestResult<ProjectCost> AddProjectCost(ProjectCost model, string projectId);

        Task<ObservableCollection<ProjectCost>> GetProjectCosts(string projectId);

        Task<ProjectRecruitment> GetProjectRecruitmentById(string projectRecruitmentId);

        Task<ObservableCollection<ProjectRecruitment>> GetProjectRecruitmentsById(string projectId);

        RequestResult<ProjectRecruitment> AddProjectRecruitment(ProjectRecruitment model, string projectId);

        RequestResult<ProjectRecruitment> UpdateProjectRecruitment(ProjectRecruitment model, string projectRecruitmentId);
    }
}