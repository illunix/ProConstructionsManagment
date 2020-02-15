namespace ProConstructionsManagment.Desktop.Views.Shell
{
    public partial class Shell
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}