using Newtonsoft.Json;

namespace Seemon.Vault.Helpers
{
    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialised = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialised);
        }

        public static bool IsNotNull<T>(this T self)
        {
            return (self != null);
        }
    }
}
