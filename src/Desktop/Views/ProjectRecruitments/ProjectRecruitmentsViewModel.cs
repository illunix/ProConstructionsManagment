using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitments
{
    public class ProjectRecruitmentsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _projectId;
        private string _projectRecruitmentsCount;

        private ObservableCollection<Models.ProjectRecruitment> _projectRecruitments;

        public ProjectRecruitmentsViewModel(IProjectsService projectsService, IPositionsService positionsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _positionsService = positionsService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public string ProjectRecruitmentsCount
        {
            get => _projectRecruitmentsCount;
            set => Set(ref _projectRecruitmentsCount, value);
        }

        public ObservableCollection<Models.ProjectRecruitment> ProjectRecruitments
        {
            get => _projectRecruitments;
            set => Set(ref _projectRecruitments, value);
        }

        public ICommand NavigateToProjectRecruitmentViewCommand => new AsyncRelayCommand<object>(NavigateToProjectRecruitmentView);

        private async Task NavigateToProjectRecruitmentView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitment));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitmentNavigation));

            if (obj is string projectRecruitmentId)
            {
                _messengerService.Send(new ProjectRecruitmentIdMessage(projectRecruitmentId));
                _messengerService.Send(new ProjectIdMessage(ProjectId));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                var projectRecruitments = await _projectsService.GetProjectRecruitmentsById(ProjectId);

                var positionIds = projectRecruitments.Select(x => x.PositionId);

                // replace position ids with positon names
                foreach (var positionId in positionIds)
                {
                    var position = await _positionsService.GetPositionById(positionId);

                    foreach (var projectRecruitment in projectRecruitments)
                    {
                        projectRecruitment.PositionId = position.Name;
                    }

                    ProjectRecruitments = projectRecruitments;
                }

                ProjectRecruitmentsCount = $"Łącznie {ProjectRecruitments.Count} rekordów";
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading project recruitments view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}
