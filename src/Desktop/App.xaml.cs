using System.Windows;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Shell;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Raw;

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

            Log.Logger = CreateLogger();

            Log.Information("Initializing Pro Constructions Managment");

            Log.Debug("Created logger");

            var shell = ViewModelLocator.Get<Shell>();

            shell.Show();
        }

        private static Logger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile(new RawFormatter(), "logs\\log.txt")
                .CreateLogger();
        }
    }
}