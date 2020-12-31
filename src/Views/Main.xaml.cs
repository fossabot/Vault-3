using MahApps.Metro.Controls;
using Seemon.Vault.ViewModels;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
