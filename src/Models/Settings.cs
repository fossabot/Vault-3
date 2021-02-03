using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class Settings : BindableBase
    {
        [JsonConstructor]
        public Settings()
        {
            Application = new ApplicationSettings();
            System = new SystemSettings();
            Clipboard = new ClipboardSettings();
            PasswordGenerator = new PasswordGeneratorSettings();
            Git = new GitSettings();
            Updates = new UpdateSettings();
            Programs = new ProgramPathSettings();
            Profiles = new ItemObservableCollection<Profile>();
        }

        [JsonProperty("application")]
        public ApplicationSettings Application { get; set; }

        [JsonProperty("system")]
        public SystemSettings System { get; set; }

        [JsonProperty("clipboard")]
        public ClipboardSettings Clipboard { get; set; }

        [JsonProperty("passwordGenerator")]
        public PasswordGeneratorSettings PasswordGenerator { get; set; }

        [JsonProperty("git")]
        public GitSettings Git { get; set; }

        [JsonProperty("updates")]
        public UpdateSettings Updates { get; set; }

        [JsonProperty("programs")]
        public ProgramPathSettings Programs { get; set; }

        [JsonProperty("profiles")]
        public ItemObservableCollection<Profile> Profiles { get; set; }
    }
}
