using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class UpdateViewModel : ViewModelBase
    {
        private GitHubRelease _release = null;
        private RelayCommand _installUpdateCommand = null;
        private int _downloadPercentage = 0;
        private long _completedBytes = 0;
        private long _totalBytes = 0;
        private string _updateStatus = string.Empty;
        private bool _showProgress = false;

        public UpdateViewModel(Window owner)
            : base(owner)
        {
            Release = UpdateManager.Instance.Release;
        }

        public ICommand InstallUpdateCommand
        {
            get
            {
                if (_installUpdateCommand == null)
                    _installUpdateCommand = RegisterCommand(OnInstallUpdate);
                return _installUpdateCommand;
            }
        }

        public GitHubRelease Release
        {
            get { return _release; }
            set { SetProperty(ref _release, value); }
        }

        public bool ShowProgress
        {
            get { return _showProgress; }
            set { SetProperty(ref _showProgress, value); }
        }

        public string CurrentVersion
        {
            get { return UpdateManager.Instance.Current.ToString(); }
        }

        public string UpdateVersion
        {
            get { return UpdateManager.Instance.Upgrade.ToString(); }
        }

        public int DownloadPercentage
        {
            get { return _downloadPercentage; }
            set { SetProperty(ref _downloadPercentage, value); }
        }

        public long CompletedBytes
        {
            get { return _completedBytes; }
            set { SetProperty(ref _completedBytes, value); }
        }

        public long TotalBytes
        {
            get { return _totalBytes; }
            set { SetProperty(ref _totalBytes, value); }
        }

        public string UpdateStatus
        {
            get { return _updateStatus; }
            set { SetProperty(ref _updateStatus, value); }
        }

        public void OnInstallUpdate(object parameter)
        {
            ShowProgress = true;
            UpdateManager.Instance.DownloadAndExtract(UpdateProgress);
        }

        private void UpdateProgress(string path, long completedBytes, long totalBytes, int downloadPercentage, string status)
        {
            TotalBytes = totalBytes;
            CompletedBytes = completedBytes;
            DownloadPercentage = downloadPercentage;
            UpdateStatus = status;
        }
    }
}
