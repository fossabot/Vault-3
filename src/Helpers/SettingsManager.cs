using Newtonsoft.Json;
using Seemon.Vault.Models;
using System.IO;
using System.Reflection;

namespace Seemon.Vault.Helpers
{
    public sealed class SettingsManager
    {
        private static SettingsManager _instance = null;
        private static readonly object _padlock = new object();

        private string _path = string.Empty;
        private Settings _settings;

        private SettingsManager()
        {
            _path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.json");
            if (!File.Exists(_path))
            {
                _settings = new Settings();
                SaveSettings(_settings);
            }
            LoadSettings();
        }

        public Settings Settings
        {
            get { return _settings; }
        }

        public static SettingsManager Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new SettingsManager();
                    return _instance;
                }
            }
        }

        public void LoadSettings()
        {
            var settingsJson = File.ReadAllText(_path);
            _settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
        }

        public void SaveSettings(bool reload = true)
        {
            SaveSettings(this._settings, reload);
        }

        public void SaveSettings(Settings settings, bool reload = true)
        {
            var settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(_path, settingsJson);
            if (reload)
                LoadSettings();
        }
    }
}
