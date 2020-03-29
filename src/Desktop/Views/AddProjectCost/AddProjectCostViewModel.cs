using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.AddProjectCost
{
    public class AddProjectCostViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;
        private readonly IMessengerService _messengerService;

        private int _projectCostGrossAmount;
        private string _projectCostDescription;

        private string _projectId;

        public AddProjectCostViewModel(IProjectsService projectsService, IShellManager shellManager, IMessengerService messengerService)
        {
            _projectsService = projectsService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public int ProjectCostGrossAmount
        {
            get => _projectCostGrossAmount;
            set => Set(ref _projectCostGrossAmount, value);
        }

        public string ProjectCostDescription
        {
            get => _projectCostDescription;
            set => Set(ref _projectCostDescription, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(ProjectCostGrossAmount.ToString()))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public ICommand AddProjectCostCommand => new AsyncRelayCommand(AddProjectCost);

        private async Task AddProjectCost()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.ProjectCost
                    {
                        Id = Guid.NewGuid().ToString(),
                        GrossAmount = ProjectCostGrossAmount,
                        CostDescription = ProjectCostDescription
                    };

                    var result = await Task.Run(() => _projectsService.AddProjectCost(data, ProjectId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully added cost to project ({data.ProjectId})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding cost to project");

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