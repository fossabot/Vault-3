using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemon.Vault.Helpers
{
    public static class Extensions
    {
        public static bool IsNotNull<T>(this T self)
        {
            return (self != null);
        }
    }
}
