using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class Settings : BindableBase
    {
        [JsonConstructor]
        public Settings()
        {
            System = new SystemSettings();
            Clipboard = new ClipboardSettings();
            PasswordGenerator = new PasswordGeneratorSettings();
            Git = new GitSettings();
            Programs = new ProgramPathSettings();
        }

        [JsonProperty("system")]
        public SystemSettings System { get; set; }
        [JsonProperty("clipboard")]
        public ClipboardSettings Clipboard { get; set; }
        [JsonProperty("passwordGenerator")]
        public PasswordGeneratorSettings PasswordGenerator { get; set; }
        [JsonProperty("git")]
        public GitSettings Git { get; set; }
        [JsonProperty("programs")]
        public ProgramPathSettings Programs { get; set; }
    }
}
