using Seemon.Vault.Helpers;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Seemon.Vault
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                if (e.Args[0] == "-Update")
                {
                    if (UpdateManager.Instance.Update(e.Args[1]))
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = Path.Combine(e.Args[1], "Vault.exe"),
                            Verb = "RunAs",
                            Arguments = "-Updated"
                        };

                        Process.Start(psi);
                        Application.Current.Shutdown();
                    }
                }
                else if (e.Args[0] == "-Updated")
                {
                    SettingsManager.Instance.Settings.Updates.ShowReleaseNotes = true;
                    SettingsManager.Instance.SaveSettings(true);
                }
            }

            var window = new Views.MainView();
            window.Show();
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {

        }
    }
}
