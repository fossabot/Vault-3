using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Seemon.Vault.Helpers
{
    public class BindableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        [JsonIgnore]
        public bool HasErrors
        {
            get
            {
                try
                {
                    var errorsCount = _errors.Values.FirstOrDefault(r => r.Count > 0);
                    if (errorsCount != null)
                        return true;
                    return false;
                }
                catch { }
                return true;
            }
        }

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Validate(propertyName);
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            var errors = new List<string>();
            if (propertyName != null)
            {
                _errors.TryGetValue(propertyName, out errors);
                return errors;
            }
            return null;
        }

        public virtual void Validate([CallerMemberName] string propertyName = null) { }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged("HasErrors");
        }
    }
}
