using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private RelayCommand _navigateCommand;
        private RelayCommand _licenseCommand;
        private About _about;

        public AboutViewModel(Window owner)
            : base(owner)
        {
            _about = new About();
        }

        public string Title
        {
            get { return _about.Title; }
        }

        public string Version
        {
            get { return string.Format("V {0}", _about.Version); }
        }

        public string Author
        {
            get { return string.Format("Developed by {0}", _about.Author); }
        }

        public string Description
        {
            get { return _about.Description; }
        }

        public string Copyright
        {
            get { return _about.Copyright; }
        }

        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                    _navigateCommand = RegisterCommand(OnNavigate);
                return _navigateCommand;
            }
        }

        public ICommand LicenseCommand
        {
            get
            {
                if (_licenseCommand == null)
                    _licenseCommand = RegisterCommand(OnLicense);
                return _licenseCommand;
            }
        }

        public void OnNavigate(object parameter)
        {
            if (parameter.IsNotNull())
                Process.Start(new ProcessStartInfo { FileName = parameter.ToString(), UseShellExecute = true });
        }

        public void OnLicense(object parameter)
        {
            var licenseWindow = new Views.License();
            licenseWindow.Owner = Owner;
            licenseWindow.ShowDialog();
        }
    }
}
