using System.Windows;

namespace Seemon.Vault.Helpers
{
    public class ViewModelBase : BindableBase
    {
        private Window _owner;
        public Window Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public ViewModelBase(Window owner)
        {
            _owner = owner;
        }

        public bool TryClose()
        {
            try
            {
                if (_owner != null)
                    Owner.Close();
                return true;
            }
            catch { }
            return false;
        }
    }
}
