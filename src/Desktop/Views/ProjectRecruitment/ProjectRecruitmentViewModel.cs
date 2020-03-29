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

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitment
{
    public class ProjectRecruitmentViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _projectId;
        private string _projectRecruitmentId;
        private string _positionId;
        private int _position;
        private int _requiredNumberOfEmployees;

        private ObservableCollection<Models.Position> _positions;
        public ProjectRecruitmentViewModel(IProjectsService projectsService, IPositionsService positionsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _positionsService = positionsService;
            _shellManager = shellManager;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
            messengerService.Register<ProjectRecruitmentIdMessage>(this, msg => ProjectRecruitmentId = msg.ProjectRecruitmentId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }


        public string ProjectRecruitmentId
        {
            get => _projectRecruitmentId;
            set => Set(ref _projectRecruitmentId, value);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        public int Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        public int RequiredNumberOfEmployees
        {
            get => _requiredNumberOfEmployees;
            set => Set(ref _requiredNumberOfEmployees, value);
        }

        public ObservableCollection<Models.Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(RequiredNumberOfEmployees.ToString()) || string.IsNullOrWhiteSpace(Position.ToString()))
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

                Positions = await _positionsService.GetAllPositions();

                var projectRecruitment = await _projectsService.GetProjectRecruitmentById(ProjectRecruitmentId);

                var position = await _positionsService.GetPositionById(projectRecruitment.PositionId);

                var positionIndex = Positions
                    .ToList()
                    .FindIndex(x => x.Id == position.Id);

                Position = positionIndex;

                RequiredNumberOfEmployees = projectRecruitment.RequiredNumberOfEmployees;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading project recruitment view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        public ICommand UpdateProjectRecruitmentCommand => new AsyncRelayCommand(UpdateProjectRecruitment);

        private async Task UpdateProjectRecruitment()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.ProjectRecruitment
                    {
                        Id = ProjectId,
                        PositionId = PositionId,
                        RequiredNumberOfEmployees = RequiredNumberOfEmployees,
                    };

                    var result = await Task.Run(() => _projectsService.UpdateProjectRecruitment(data, ProjectId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully edited project recruitment ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano rekrutacje");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed editing project recruitment");

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
