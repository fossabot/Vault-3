using Newtonsoft.Json;
using Seemon.Vault.Helpers;
using System.IO;
using System.Runtime.CompilerServices;

namespace Seemon.Vault.Models
{
    public class Profile : BindableBase
    {
        private string _name;
        private string _location;
        private bool _isDefault = false;

        [JsonConstructor]
        public Profile()
        {
            _isDefault = false;
        }

        [JsonProperty("name")]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return _location;
                return _name;
            }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty("location")]
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        [JsonProperty("isDefault")]
        public bool IsDefault
        {
            get { return _isDefault; }
            set { SetProperty(ref _isDefault, value); }
        }

        [JsonIgnore]
        public bool IsSelected { get; set; }

        public override void Validate([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "Location")
            {
                ClearErrors(nameof(Location));
                if (!string.IsNullOrEmpty(Location) && !Directory.Exists(Location))
                    AddError(nameof(Location), "Please enter or select a valid path for Location.");
            }
            base.Validate(propertyName);
        }
    }
}
