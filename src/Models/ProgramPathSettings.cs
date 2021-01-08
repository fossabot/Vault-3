using Newtonsoft.Json;
using Seemon.Vault.Helpers;
using System.IO;
using System.Runtime.CompilerServices;

namespace Seemon.Vault.Models
{
    public class ProgramPathSettings : BindableBase
    {
        private string _gpg = string.Empty;
        private string _git = string.Empty;

        [JsonProperty("gpg")]
        public string Gpg
        {
            get { return _gpg; }
            set { SetProperty(ref _gpg, value); }
        }

        [JsonProperty("git")]
        public string Git
        {
            get { return _git; }
            set { SetProperty(ref _git, value); }
        }

        public override void Validate([CallerMemberName] string propertyName = null)
        {
            switch (propertyName)
            {
                case "Gpg":
                    ValidateProperty(nameof(Gpg), Gpg);
                    break;
                case "Git":
                    ValidateProperty(nameof(Git), Git);
                    break;
                default:
                    break;
            }
            base.Validate(propertyName);
        }

        private void ValidateProperty(string name, string value)
        {
            ClearErrors(name);
            if (!string.IsNullOrEmpty(value) && !File.Exists(value))
                AddError(name, $"Please enter or select a valid path for {name} executable.");
        }
    }
}
