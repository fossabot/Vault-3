using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class PasswordGeneratorSettings : BindableBase
    {
        private int _length = 8;
        [JsonProperty("length")]
        public int Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }

        private bool _includeUppercase = true;
        [JsonProperty("includeUppercase")]
        public bool IncludeUppercase
        {
            get { return _includeUppercase; }
            set { SetProperty(ref _includeUppercase, value); }
        }

        private bool _includeLowercase = true;
        [JsonProperty("includeLowercase")]
        public bool IncludeLowercase
        {
            get { return _includeLowercase; }
            set { SetProperty(ref _includeLowercase, value); }
        }

        private bool _includeNumerals = true;
        [JsonProperty("includeNumerals")]
        public bool IncludeNumerals
        {
            get { return _includeNumerals; }
            set { SetProperty(ref _includeNumerals, value); }
        }

        private bool _includeSpace = false;
        [JsonProperty("includeSpace")]
        public bool IncludeSpace
        {
            get { return _includeSpace; }
            set { SetProperty(ref _includeSpace, value); }
        }

        private bool _includeSpecial = true;
        [JsonProperty("includeSpecial")]
        public bool IncludeSpecial
        {
            get { return _includeSpecial; }
            set { SetProperty(ref _includeSpecial, value); }
        }

        private string _excludeCharacters = string.Empty;
        [JsonProperty("excludeCharacters")]
        public string ExcludeCharacters
        {
            get { return _excludeCharacters; }
            set { SetProperty(ref _excludeCharacters, value); }
        }
    }
}
