using Newtonsoft.Json.Linq;
using RestSharp;
using Seemon.Vault.Helpers;

namespace Seemon.Vault.Models
{
    public class GitHubRelease : BindableBase
    {
        private const string _markdownUrl = "https://api.github.com/markdown";

        private JObject _releaseObject;
        private string _releaseUrl;
        private string _releaseName;
        private string _mdContents;
        private string _htmlContents;
        private string _tag;
        private GitHubVersion _version;

        public GitHubRelease(string releaseJson)
        {
            _releaseObject = JObject.Parse(releaseJson);
            LoadRelease();
        }

        public string Url
        {
            get { return _releaseUrl; }
            set { SetProperty(ref _releaseUrl, value); }
        }

        public string Name
        {
            get { return _releaseName; }
            set { SetProperty(ref _releaseName, value); }
        }

        public string Contents
        {
            get { return _htmlContents; }
            set { SetProperty(ref _htmlContents, value); }
        }

        public GitHubVersion Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        public ItemObservableCollection<GitHubAsset> Assets { get; set; }

        private void LoadRelease()
        {
            _tag = _releaseObject["tag_name"].ToString();
            _mdContents = _releaseObject["body"].ToString();

            Name = _releaseObject["name"].ToString();
            Url = _releaseObject["html_url"].ToString();
            Version = new GitHubVersion(_tag);

            var client = new RestClient(_markdownUrl);

            var request = new RestRequest();
            request.AddJsonBody(new { text = _mdContents });

            var response = client.Post(request);
            Contents = response.Content;

            var assets = _releaseObject.SelectTokens("$.assets[*]");

            Assets = new ItemObservableCollection<GitHubAsset>();
            foreach (var asset in assets)
                Assets.Add(new GitHubAsset(asset.ToString()));
        }
    }
}
