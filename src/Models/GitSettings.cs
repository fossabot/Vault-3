using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class GitSettings : BindableBase
    {
        private bool _useGit = false;
        private bool _autoAddGpgIdFiles = false;
        private bool _autoPush = false;
        private bool _autoPull = false;

        [JsonProperty("useGit")]
        public bool UseGit
        {
            get { return _useGit; }
            set { SetProperty(ref _useGit, value); }
        }

        [JsonProperty("autoAddGpgIdFiles")]
        public bool AutoAddGpgIdFiles
        {
            get { return _autoAddGpgIdFiles; }
            set { SetProperty(ref _autoAddGpgIdFiles, value); }
        }

        [JsonProperty("autoPush")]
        public bool AutoPush
        {
            get { return _autoPush; }
            set { SetProperty(ref _autoPush, value); }
        }

        [JsonProperty("autoPull")]
        public bool AutoPull
        {
            get { return _autoPull; }
            set { SetProperty(ref _autoPull, value); }
        }
    }
}
