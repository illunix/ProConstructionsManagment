using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IShellManager _shellManager;

        private string _employeesCount;

        private bool _isLoadingData;

        public MainViewModel(IShellManager shellManager)
        {
            _shellManager = shellManager;
        }

        public bool IsLoadingData
        {
            get => _isLoadingData;
            set => Set(ref _isLoadingData, value);
        }

        public string EmployeesCount
        {
            get => _employeesCount;
            set => Set(ref _employeesCount, value);
        }

        public async Task InitializeAsync()
        {
            _shellManager.SetLoadingData(true);

            _shellManager.SetLoadingData(false);
        }
    }
}