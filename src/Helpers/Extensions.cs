using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Seemon.Vault.Helpers
{
    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static ItemObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
            where T : INotifyPropertyChanged
        {
            var col = new ItemObservableCollection<T>();
            foreach (T current in enumerable)
            {
                col.Add(current);
            }
            return col;
        }

        public static bool IsNotNull<T>(this T self)
        {
            return (self != null);
        }
    }
}
