using System;
using System.Reflection;

namespace Seemon.Vault.Models
{
    public class About
    {
        public About()
            : this(Assembly.GetExecutingAssembly())
        { }

        public About(Assembly assembly)
        {
            Title = GetAssemblyAttribute<AssemblyTitleAttribute>(assembly)?.Title ?? "Vault";
            Version = GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(assembly)?.InformationalVersion ?? string.Empty;
            Author = GetAssemblyAttribute<AssemblyCompanyAttribute>(assembly)?.Company ?? "Matt Seemon";
            Description = GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly)?.Description ?? "A GnuPG powered, file system based secrets repository.";
            Copyright = GetAssemblyAttribute<AssemblyCopyrightAttribute>(assembly)?.Copyright ?? "© Copyright 2020, Matt Seemon. All rights reserved.";
        }

        public string Title { get; set; }

        public string Version { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Copyright { get; set; }

        public static T GetAssemblyAttribute<T>(Assembly assembly)
            where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), true);

            if ((attributes == null) || (attributes.Length == 0))
                return null;

            return (T)attributes[0];
        }
    }
}
