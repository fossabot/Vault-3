using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Seemon.Vault.Helpers
{
    public class ItemObservableCollection<T>
        : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RegisterPropertyChanged(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    UnRegisterPropertyChanged(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    UnRegisterPropertyChanged(e.OldItems);
                    RegisterPropertyChanged(e.NewItems);
                    break;
            }
            base.OnCollectionChanged(e);
        }

        protected override void ClearItems()
        {
            UnRegisterPropertyChanged(this);
            base.ClearItems();
        }

        public void Replace(T oldItem, T newItem)
        {
            var index = -1;
            for (var i = 0; i < this.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(oldItem, this[i]))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
                this.SetItem(index, newItem);
            else
                throw new System.Exception("Collection has changed since you updated. Please refresh the collection and try again..");
        }

        private void RegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                    item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
            }
        }

        private void UnRegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                    item.PropertyChanged -= new PropertyChangedEventHandler(OnItemPropertyChanged);
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
            => base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
