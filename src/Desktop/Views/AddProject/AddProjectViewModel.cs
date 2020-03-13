using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.AddProject
{
    public class AddProjectViewModel : ViewModelBase
    {
        private readonly IClientsService _clientsService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;
        private string _client;

        private ObservableCollection<Models.Client> _clients;

        private bool _projectAgreement;
        private string _projectEndDate;

        private string _projectName;
        private string _projectPlaceOfPerformance;
        private int _projectRequiredNumberOfEmployees;
        private string _projectStartDate;

        public AddProjectViewModel(IProjectsService projectsService, IClientsService clientsService,
            IShellManager shellManager)
        {
            _projectsService = projectsService;
            _clientsService = clientsService;
            _shellManager = shellManager;
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

        public string Client
        {
            get => _client;
            set => Set(ref _client, value);
        }

        public ObservableCollection<Models.Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        public bool ProjectAgreement
        {
            get => _projectAgreement;
            set => Set(ref _projectAgreement, value);
        }
        
        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(ProjectStartDate) ||
                string.IsNullOrWhiteSpace(ProjectEndDate) ||
                string.IsNullOrWhiteSpace(ProjectPlaceOfPerformance)) return new ValidationResult(false);

            return new ValidationResult(true);
        }

        public async Task Initialize()
        {
            Clients = await _clientsService.GetAllClients();
        }
        
        public ICommand AddProjectCommand => new AsyncRelayCommand(AddProject);

        private async Task AddProject()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Project
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = ProjectName,
                        StartDate = ProjectStartDate,
                        EndDate = ProjectEndDate,
                        PlaceOfPerformance = ProjectPlaceOfPerformance,
                        RequiredNumberOfEmployees = ProjectRequiredNumberOfEmployees,
                        Agreement = ProjectAgreement,
                        Status = 0
                    };

                    var result = await Task.Run(() => _projectsService.AddProject(data));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully updated project ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed updating project");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
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