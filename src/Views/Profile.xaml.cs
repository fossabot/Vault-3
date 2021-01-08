using MahApps.Metro.Controls;
using Seemon.Vault.Helpers;
using Seemon.Vault.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : MetroWindow
    {
        public Profile(Models.Profile profile = null)
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel(this, profile);
        }
    }
}
