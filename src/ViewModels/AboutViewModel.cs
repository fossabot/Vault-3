using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System.Diagnostics;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class AboutViewModel : BindableBase
    {

        public AboutViewModel()
        {
            _about = new About();
        }

        private About _about;

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

        private RelayCommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                    _navigateCommand = new RelayCommand(OnNavigate);
                return _navigateCommand;
            }
        }

        public void OnNavigate(object parameter)
        {
            if (parameter.IsNotNull())
                Process.Start(new ProcessStartInfo { FileName = parameter.ToString(), UseShellExecute = true });
        }
    }
}