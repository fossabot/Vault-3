using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class ApplicationSettings : BindableBase
    {
        private bool _firstRun = false;

        [JsonProperty("firstRun")]
        public bool FirstRun
        {
            get { return _firstRun; }
            set { SetProperty(ref _firstRun, value); }
        }
    }
}
