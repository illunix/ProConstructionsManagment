using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Project
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IClientsService _clientsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _projectId;

        private string _clientId;
        private int _client;
        private ObservableCollection<Models.Client> _clients;

        private bool _projectAgreement;
        private string _projectEndDate;

        private string _projectName;
        private string _projectPlaceOfPerformance;
        private int _projectRequiredNumberOfEmployees;
        private string _projectStartDate;

        public ProjectViewModel(IProjectsService projectsService, IClientsService clientsService, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _projectsService = projectsService;
            _clientsService = clientsService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public string ProjectName
        {
            get => _projectName;
            set => Set(ref _projectName, value);
        }

        public string ProjectStartDate
        {
            get => _projectStartDate;
            set => Set(ref _projectStartDate, value);
        }

        public string ProjectEndDate
        {
            get => _projectEndDate;
            set => Set(ref _projectEndDate, value);
        }

        public string ProjectPlaceOfPerformance
        {
            get => _projectPlaceOfPerformance;
            set => Set(ref _projectPlaceOfPerformance, value);
        }

        public int ProjectRequiredNumberOfEmployees
        {
            get => _projectRequiredNumberOfEmployees;
            set => Set(ref _projectRequiredNumberOfEmployees, value);
        }

        public bool ProjectAgreement
        {
            get => _projectAgreement;
            set => Set(ref _projectAgreement, value);
        }

        public string ClientId
        {
            get => _clientId;
            set => Set(ref _clientId, value);
        }

        public int Client
        {
            get => _client;
            set => Set(ref _client, value);
        }

        public ObservableCollection<Models.Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(ProjectStartDate) ||
                string.IsNullOrWhiteSpace(ProjectEndDate) ||
                string.IsNullOrWhiteSpace(ProjectPlaceOfPerformance))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                Clients = await _clientsService.GetAllClients();

                var project = await _projectsService.GetProjectById(ProjectId);

                var clientIndex = Clients
                    .ToList()
                    .FindIndex(x => x.Id == project.ClientId);

                Client = clientIndex;

                ProjectName = project.Name;
                ProjectStartDate = project.StartDate;
                ProjectEndDate = project.EndDate;
                ProjectPlaceOfPerformance = project.PlaceOfPerformance;
                ProjectRequiredNumberOfEmployees = project.RequiredNumberOfEmployees;
                ProjectAgreement = project.Agreement;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading project view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        public ICommand UpdateProjectCommand => new AsyncRelayCommand(UpdateProject);

        private async Task UpdateProject()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Project
                    {
                        Id = ProjectId,
                        ClientId = ClientId,
                        Name = ProjectName,
                        StartDate = ProjectStartDate,
                        EndDate = ProjectEndDate,
                        PlaceOfPerformance = ProjectPlaceOfPerformance,
                        RequiredNumberOfEmployees = ProjectRequiredNumberOfEmployees,
                        Agreement = ProjectAgreement,
                        Status = 0
                    };

                    var result = await Task.Run(() => _projectsService.UpdateProject(data, ProjectId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully edited project ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano projekt");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed editing project");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas dodawania projektu, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
                }
                finally
                {
                    _shellManager.SetLoadingData(false);
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola");
            }
        }
    }
}