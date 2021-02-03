using MahApps.Metro.Controls;
using Seemon.Vault.ViewModels;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this);
        }

        private void OnWindowLoaded(object sender, System.Windows.RoutedEventArgs e) => flyoutAbout.Content = new AboutView();
    }
}
