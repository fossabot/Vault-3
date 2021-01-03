using Newtonsoft.Json;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class SystemSettings : BindableBase
    {
        private bool _startWithWindows = false;
        [JsonProperty("startWithWindows")]
        public bool StartWithWindows
        {
            get { return _startWithWindows; }
            set { SetProperty(ref _startWithWindows, value); }
        }

        private bool _alwaysOnTop = false;
        [JsonProperty("alwaysOnTop")]
        public bool AlwaysOnTop
        {
            get { return _alwaysOnTop; }
            set { SetProperty(ref _alwaysOnTop, value); }
        }

        private bool _showVaultInNotifications = false;
        [JsonProperty("showVaultInNotifications")]
        public bool ShowVaultInNotifications
        {
            get { return _showVaultInNotifications; }
            set { SetProperty(ref _showVaultInNotifications, value); }
        }

        private bool _minimizeToNotifications = false;
        [JsonProperty("minimizeToNotifications")]
        public bool MinimizeToNotifications
        {
            get { return _minimizeToNotifications; }
            set { SetProperty(ref _minimizeToNotifications, value); }
        }

        private bool _closeToNotifications = false;
        [JsonProperty("closeToNotifications")]
        public bool CloseToNotifications
        {
            get { return _closeToNotifications; }
            set { SetProperty(ref _closeToNotifications, value); }
        }
    }
}
