using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Shell;
using System.Windows;

namespace Desktop
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var shell = ViewModelLocator.Get<Shell>();

            shell.Show();
        }
    }
}