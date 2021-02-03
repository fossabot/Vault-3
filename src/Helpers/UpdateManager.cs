using Newtonsoft.Json.Linq;
using RestSharp;
using Seemon.Vault.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Seemon.Vault.Helpers
{
    public class UpdateManager
    {
        private static UpdateManager _instance = null;
        private static readonly object _padlock = new object();

        private const string _releasesUrl = "https://api.github.com/repos/mattseemon/vault/releases";
        private Duration _updateTimeout = TimeSpan.FromSeconds(30);

        private GitHubVersion _currentVersion = null;
        private GitHubVersion _upgradeVersion = null;
        private GitHubRelease _release = null;

        private SettingsManager _settingsManager;

        private UpdateManager()
        {
            _settingsManager = SettingsManager.Instance;
        }

        public static UpdateManager Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new UpdateManager();
                    return _instance;
                }
            }
        }

        public GitHubVersion Current
        {
            get
            {
                if (_currentVersion == null)
                    _currentVersion = new GitHubVersion(new About().Version);
                return _currentVersion;
            }
        }

        public GitHubVersion Upgrade
        {
            get { return _upgradeVersion; }
        }

        public GitHubRelease Release
        {
            get { return _release; }
        }

        public ItemObservableCollection<GitHubRelease> Releases { get; set; }

        public async Task<bool> CheckForUpdates()
        {
            var nextVersion = Current;

            var client = new RestClient(_releasesUrl);
            var response = await client.ExecuteGetAsync(new RestRequest(Method.GET));

            var releases = JArray.Parse(response.Content).SelectTokens("$.[?(@.prerelease == false)]");

            if (_settingsManager.Settings.Updates.IncludePreReleases)
                releases = JArray.Parse(response.Content).SelectTokens("$.[*]");

            Releases = new ItemObservableCollection<GitHubRelease>();

            foreach (var release in releases)
            {
                var temp = new GitHubRelease(release.ToString());
                Releases.Add(temp);

                if (temp.Version > nextVersion)
                    nextVersion = temp.Version;
            }

            _settingsManager.Settings.Updates.LastChecked = DateTime.Now;
            _settingsManager.SaveSettings(true);

            if (nextVersion == Current)
                return false;

            _upgradeVersion = nextVersion;
            _release = Releases.First(x => x.Version == _upgradeVersion);

            return true;
        }

        public void DownloadAndExtract(Action<string, long, long, int, string> updateProgress)
        {
            var asset = _release.Assets.First(x => x.Name.Contains("release"));
            var outputPath = Path.Join(Path.GetTempPath(), asset.Name);

            WebClient client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                updateProgress(outputPath, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage, "Downloading update...");
            };

            client.DownloadFileCompleted += (sender, e) =>
            {
                updateProgress(outputPath, asset.Size, asset.Size, 100, "Installing update...");
                var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var extractionPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

                ZipFile.ExtractToDirectory(outputPath, extractionPath);

                var files = Directory.GetFiles(extractionPath, "vault.exe", SearchOption.AllDirectories);
                var updatePath = (new FileInfo(files[0])).DirectoryName;

                File.Copy(Path.Combine(applicationPath, "settings.json"), Path.Combine(updatePath, "settings.json"));
                File.WriteAllText(Path.Combine(updatePath, "release.html"), Release.Contents);

                updateProgress(updatePath, asset.Size, asset.Size, 100, "Restarting applcation...");

                var psi = new ProcessStartInfo
                {
                    FileName = files[0],
                    Arguments = $"-Update \"${applicationPath}\"",
                    Verb = "RunAs"
                };

                Process.Start(psi);
                Application.Current.Shutdown();
            };

            client.DownloadFileAsync(new Uri(asset.DownloadUrl), outputPath);
        }

        public bool Update(string destination)
        {
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var success = false;

            var startTime = DateTime.Now;
            var backupPath = $"{destination}-backup";

            while (DateTime.Now - startTime < _updateTimeout && !success)
            {
                try
                {
                    if (Directory.Exists(destination))
                        Directory.Move(destination, backupPath);

                    Directory.CreateDirectory(destination);

                    success = CopyDirectory(applicationPath, destination);
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }

            if (!success)
            {
                if (Directory.Exists(backupPath))
                    Directory.Move(backupPath, destination);
            }
            else
                Directory.Delete(backupPath, true);

            return success;
        }

        public bool CopyDirectory(string source, string destination)
        {
            try
            {
                foreach (string path in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(path.Replace(source, destination));

                foreach (string path in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
                    File.Copy(path, path.Replace(source, destination), true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
