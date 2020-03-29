using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.AddProjectRecruitment
{
    public class AddProjectRecruitmentViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private ObservableCollection<Models.Position> _positions;

        private string _positionId;
        private string _position;
        private int _requiredNumberOfEmployees;

        private string _projectId;

        public AddProjectRecruitmentViewModel(IProjectsService projectsService, IPositionsService positionsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _positionsService = positionsService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public ObservableCollection<Models.Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        public string Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        public int RequiredNumberOfEmployees
        {
            get => _requiredNumberOfEmployees;
            set => Set(ref _requiredNumberOfEmployees, value);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(RequiredNumberOfEmployees.ToString()) || string.IsNullOrWhiteSpace(Position))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public async Task Initialize()
        {
            Positions = await _positionsService.GetAllPositions();
        }

        public ICommand AddProjectRecruitmentCommand => new AsyncRelayCommand(AddProjectRecruitment);

        private async Task AddProjectRecruitment()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.ProjectRecruitment
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProjectId = ProjectId,
                        PositionId = PositionId,
                        RequiredNumberOfEmployees = RequiredNumberOfEmployees
                    };

                    var result = await Task.Run(() => _projectsService.AddProjectRecruitment(data, ProjectId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully addded project recrutation ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding project recruitment");

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
