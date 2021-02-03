using MahApps.Metro.Controls;
using Seemon.Vault.ViewModels;
using System.Windows;

namespace Seemon.Vault.Views
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class UpdateView : MetroWindow
    {
        public UpdateView()
        {
            InitializeComponent();

            DataContext = new UpdateViewModel(this);
        }
    }
}
