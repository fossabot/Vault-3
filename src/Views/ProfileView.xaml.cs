using MahApps.Metro.Controls;
using Seemon.Vault.ViewModels;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class ProfileView : MetroWindow
    {
        public ProfileView(Models.Profile profile = null)
        {
            InitializeComponent();
            
            DataContext = new ProfileViewModel(this, profile);
        }
    }
}
