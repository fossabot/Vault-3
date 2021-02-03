using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Seemon.Vault.Helpers.Converters
{
    public class SizeFormatConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => FormatSize(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        private object FormatSize(object value)
        {
            if (!(value is long))
                return string.Empty;

            var objValue = (long)value;

            if (objValue < 512)
                return $"{objValue} Bytes";

            var temp = Math.Round((double)(objValue / 1204), 2);
            if (temp < 1024)
                return $"{temp} KB";

            temp = Math.Round((double)(objValue / Math.Pow(1024, 2)), 2);
            if (temp < 1024)
                return $"{temp} MB";

            temp = Math.Round((double)(objValue / Math.Pow(1024, 3)), 2);
            return $"{temp} GB";
        }
    }
}
