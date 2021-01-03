using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class GitSettings : BindableBase
    {
        private bool _useGit = false;
        [JsonProperty("_useGit")]
        public bool UseGit
        {
            get { return _useGit; }
            set { SetProperty(ref _useGit, value); }
        }

        private bool _autoAddGpgIdFiles = false;
        [JsonProperty("autoAddGpgIdFiles")]
        public bool AutoAddGpgIdFiles
        {
            get { return _autoAddGpgIdFiles; }
            set { SetProperty(ref _autoAddGpgIdFiles, value); }
        }

        private bool _autoPush = false;
        [JsonProperty("autoPush")]
        public bool AutoPush
        {
            get { return _autoPush; }
            set { SetProperty(ref _autoPush, value); }
        }

        private bool _autoPull = false;
        [JsonProperty("autoPull")]
        public bool AutoPull
        {
            get { return _autoPull; }
            set { SetProperty(ref _autoPull, value); }
        }
    }
}
