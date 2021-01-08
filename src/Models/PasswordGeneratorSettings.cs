using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class PasswordGeneratorSettings : BindableBase
    {
        private int _length = 8;
        private bool _includeUppercase = true;
        private bool _includeLowercase = true;
        private bool _includeNumerals = true;
        private bool _includeSpace = false;
        private bool _includeSpecial = true;
        private string _excludeCharacters = string.Empty;

        [JsonProperty("length")]
        public int Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }

        [JsonProperty("includeUppercase")]
        public bool IncludeUppercase
        {
            get { return _includeUppercase; }
            set { SetProperty(ref _includeUppercase, value); }
        }

        [JsonProperty("includeLowercase")]
        public bool IncludeLowercase
        {
            get { return _includeLowercase; }
            set { SetProperty(ref _includeLowercase, value); }
        }

        [JsonProperty("includeNumerals")]
        public bool IncludeNumerals
        {
            get { return _includeNumerals; }
            set { SetProperty(ref _includeNumerals, value); }
        }

        [JsonProperty("includeSpace")]
        public bool IncludeSpace
        {
            get { return _includeSpace; }
            set { SetProperty(ref _includeSpace, value); }
        }

        [JsonProperty("includeSpecial")]
        public bool IncludeSpecial
        {
            get { return _includeSpecial; }
            set { SetProperty(ref _includeSpecial, value); }
        }

        [JsonProperty("excludeCharacters")]
        public string ExcludeCharacters
        {
            get { return _excludeCharacters; }
            set { SetProperty(ref _excludeCharacters, value); }
        }
    }
}
