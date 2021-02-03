using Seemon.Vault.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Seemon.Vault.ViewModels
{
    public class LicenseViewModel : ViewModelBase
    {
        private List<string> _lines;

        public LicenseViewModel(Window owner)
            : base(owner)
        {
            _lines = File.ReadLines(@".\LICENSE").ToList();
        }

        public List<string> Lines
        {
            get { return _lines; }
        }
    }
}
