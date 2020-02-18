using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IProjectsService
    {
        Task<ObservableCollection<Project>> GetAllProjects();

        Task<ObservableCollection<Project>> GetStartedProjects();

        Task<ObservableCollection<Project>> GetProjectsForStart();

        Task<ObservableCollection<Project>> GetProjectsForSettlement();

        Task<ObservableCollection<Project>> GetSettledProjects();

        Task<ObservableCollection<Project>> GetEndedProjects();
        
        Task<ObservableCollection<Project>> GetProjectById(Guid projectId);

        Task<Project> AddEmployee(Project model);

        Task<Project> UpdateEmployee(Project model, Guid projectId);
    }
}