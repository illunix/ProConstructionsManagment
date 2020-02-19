using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.ProjectSettlements
{
    public class ProjectSettlementsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        
        private string _projectForSettlementCount;

        private bool _showNoData;
        
        private ObservableCollection<Models.Project> _projectsForSettlement;
        
        public ProjectSettlementsViewModel(IProjectsService projectsService, IMessengerService messengerService, IShellManager shellManager)
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
        
        public ObservableCollection<Project> ProjectsForSettlement
        {
            get => _projectsForSettlement;
            set => Set(ref _projectsForSettlement, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            ProjectsForSettlement = await _projectsService.GetProjectsForSettlement();

            ProjectForSettlementCount = $"Łącznie {ProjectsForSettlement.Count} rekordów";
            
            if (ProjectsForSettlement.Count == 0)
            {
                _messengerService.Send(new NoDataMessage(true));
            }

            _shellManager.SetLoadingData(false);
        }
    }
}