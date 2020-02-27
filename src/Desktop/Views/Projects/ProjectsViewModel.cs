﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Projects
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private string _projectCount;

        private ObservableCollection<Models.Project> _projects;

        public ProjectsViewModel(IProjectsService projectsService, IMessengerService messengerService,
            IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string ProjectCount
        {
            get => _projectCount;
            set => Set(ref _projectCount, value);
        }

        public ObservableCollection<Models.Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                Projects = await _projectsService.GetAllProjects();

                ProjectCount = $"Łącznie {Projects.Count} rekordów";

                if (Projects.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading projects view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}