using Seemon.Vault.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();   
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e) => DataContext = new AboutViewModel(Window.GetWindow(this));
    }
}
