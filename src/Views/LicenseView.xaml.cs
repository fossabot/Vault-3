using MahApps.Metro.Controls;
using Seemon.Vault.ViewModels;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class LicenseView : MetroWindow
    {
        public LicenseView()
        {
            InitializeComponent();
            this.DataContext = new LicenseViewModel(this);
        }
    }
}
