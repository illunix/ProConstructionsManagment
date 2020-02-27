using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.ProjectSettlements
{
    public class ProjectSettlementsViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private string _projectForSettlementCount;

        private ObservableCollection<Models.Project> _projectsForSettlement;

        private bool _showNoData;

        public ProjectSettlementsViewModel(IProjectsService projectsService, IMessengerService messengerService,
            IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string ProjectForSettlementCount
        {
            get => _projectForSettlementCount;
            set => Set(ref _projectForSettlementCount, value);
        }

        public ObservableCollection<Models.Project> ProjectsForSettlement
        {
            get => _projectsForSettlement;
            set => Set(ref _projectsForSettlement, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                ProjectsForSettlement = await _projectsService.GetProjectsForSettlement();

                ProjectForSettlementCount = $"Łącznie {ProjectsForSettlement.Count} rekordów";

                if (ProjectsForSettlement.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading projects settlements view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}