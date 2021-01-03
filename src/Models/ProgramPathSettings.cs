using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class ProgramPathSettings : BindableBase
    {
        private string _gpg = string.Empty;
        [JsonProperty("gpg")]
        public string Gpg
        {
            get { return _gpg; }
            set { SetProperty(ref _gpg, value); }
        }

        private string _git = string.Empty;
        [JsonProperty("git")]
        public string Git
        {
            get { return _git; }
            set { SetProperty(ref _git, value); }
        }
    }
}
