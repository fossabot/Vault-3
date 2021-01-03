﻿using Newtonsoft.Json;
using Seemon.Vault.Models;
using System.IO;
using System.Reflection;

namespace Seemon.Vault.Helpers
{
    public sealed class SettingsManager
    {
        public static SettingsManager _instance = null;
        public static readonly object _padlock = new object();

        private string _path = string.Empty;
        private Settings _settings;
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

        public void LoadSettings()
        {
            var settingsJson = File.ReadAllText(_path);
            _settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
        }

        public void SaveSettings(Settings settings)
        {
            var settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(_path, settingsJson);
            LoadSettings();
        }
    }
}
