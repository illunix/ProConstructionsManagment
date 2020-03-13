using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.AddProjectCost
{
    public class AddProjectCostNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        private string _projectId;

        public AddProjectCostNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public ICommand NavigateToProjectCostsViewCommand => new AsyncRelayCommand(NavigateToProjectCostsView);

        private async Task NavigateToProjectCostsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectCosts));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectCostsNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }
    }
}
