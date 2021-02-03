using System;

namespace Seemon.Vault.Models
{
    public class GitHubVersion : IComparable<GitHubVersion>
    {
        public GitHubVersion(string version)
        {
            Tag = version;

            if (version.StartsWith("v"))
                version = version.Substring(1);

            PreRelease = string.Empty;
            IsPreRelease = false;

            var temp = version.Split("-");
            if (temp.Length > 1)
            {
                PreRelease = temp[1];
                IsPreRelease = true;
            }

            temp = temp[0].Split(".");
            Major = int.Parse(temp[0]);
            Minor = int.Parse(temp[1]);
            Patch = int.Parse(temp[2]);
        }

        public string Tag { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public int Patch { get; set; }

        public string PreRelease { get; set; }

        public bool IsPreRelease { get; set; }

        public int CompareTo(GitHubVersion other)
        {
            if (Major.CompareTo(other.Major) != 0)
            {
                return Major.CompareTo(other.Major);
            }
            else if (Minor.CompareTo(other.Minor) != 0)
            {
                return Minor.CompareTo(other.Minor);
            }
            else if (Patch.CompareTo(other.Patch) != 0)
            {
                return Patch.CompareTo(other.Patch);
            }
            return 0;
        }

        public static bool operator <(GitHubVersion left, GitHubVersion right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(GitHubVersion left, GitHubVersion right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(GitHubVersion left, GitHubVersion right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(GitHubVersion left, GitHubVersion right)
        {
            return left.CompareTo(right) >= 0;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}-{PreRelease}";
        }
    }
}
