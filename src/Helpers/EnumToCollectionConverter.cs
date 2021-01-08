using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Seemon.Vault.Helpers
{
    [ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetAllValuesAndDescriptions(value.GetType());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }

    public class ValueDescription
    {
        public object Value { get; set; }
        public object Description { get; set; }
    }
}
