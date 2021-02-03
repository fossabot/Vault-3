using Newtonsoft.Json.Linq;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class GitHubAsset : BindableBase
    {
        private JObject _asset;
        private string _name;
        private string _downloadUrl;
        private uint _size;

        public GitHubAsset(string assetJson)
        {
            _asset = JObject.Parse(assetJson);

            Name = _asset["name"].ToString();
            DownloadUrl = _asset["browser_download_url"].ToString();
            Size = uint.Parse(_asset["size"].ToString());
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { SetProperty(ref _downloadUrl, value); }
        }

        public uint Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }
    }
}
