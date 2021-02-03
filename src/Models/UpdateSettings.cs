using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Seemon.Vault.Helpers;
using System;
using System.ComponentModel;

namespace Seemon.Vault.Models
{
    public class UpdateSettings : BindableBase
    {
        public enum CheckUpdateOptions
        {
            [Description("Every time I start the application")]
            ApplicationStart,
            [Description("Once a week")]
            Weekly,
            [Description("Once every two weeks")]
            BiWeekly,
            [Description("Once a month")]
            Monthly
        }

        private bool _autoUpdate = true;
        private CheckUpdateOptions _checkUpdates = CheckUpdateOptions.ApplicationStart;
        private bool _includePreReleases = false;
        private bool _showReleaseNotes = false;
        private DateTime _lastChecked;
        private DateTime _lastUpdated;

        [JsonProperty("autoUpdates")]
        public bool AutoUpdate
        {
            get { return _autoUpdate; }
            set { SetProperty(ref _autoUpdate, value); }
        }

        [JsonProperty("checkUpdates")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CheckUpdateOptions CheckUpdates
        {
            get { return _checkUpdates; }
            set { SetProperty(ref _checkUpdates, value); }
        }

        [JsonProperty("includePreReleases")]
        public bool IncludePreReleases
        {
            get { return _includePreReleases; }
            set { SetProperty(ref _includePreReleases, value); }
        }

        [JsonProperty("showReleaseNotes")]
        public bool ShowReleaseNotes
        {
            get { return _showReleaseNotes; }
            set { SetProperty(ref _showReleaseNotes, value); }
        }

        [JsonProperty("lastChecked")]
        public DateTime LastChecked
        {
            get { return _lastChecked; }
            set { SetProperty(ref _lastChecked, value); }
        }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }
    }
}
