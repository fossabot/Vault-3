using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class SystemSettings : BindableBase
    {
        private bool _startWithWindows = false;
        private bool _alwaysOnTop = false;
        private bool _showVaultInNotifications = false;
        private bool _minimizeToNotifications = false;
        private bool _closeToNotifications = false;

        [JsonProperty("startWithWindows")]
        public bool StartWithWindows
        {
            get { return _startWithWindows; }
            set { SetProperty(ref _startWithWindows, value); }
        }

        [JsonProperty("alwaysOnTop")]
        public bool AlwaysOnTop
        {
            get { return _alwaysOnTop; }
            set { SetProperty(ref _alwaysOnTop, value); }
        }

        [JsonProperty("showVaultInNotifications")]
        public bool ShowVaultInNotifications
        {
            get { return _showVaultInNotifications; }
            set { SetProperty(ref _showVaultInNotifications, value); }
        }

        [JsonProperty("minimizeToNotifications")]
        public bool MinimizeToNotifications
        {
            get { return _minimizeToNotifications; }
            set { SetProperty(ref _minimizeToNotifications, value); }
        }

        [JsonProperty("closeToNotifications")]
        public bool CloseToNotifications
        {
            get { return _closeToNotifications; }
            set { SetProperty(ref _closeToNotifications, value); }
        }
    }
}
