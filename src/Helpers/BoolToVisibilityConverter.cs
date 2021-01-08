using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Seemon.Vault.Helpers
{
    class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Collapsed;

            var objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => GetVisibility(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
