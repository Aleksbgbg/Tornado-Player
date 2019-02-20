namespace Tornado.Player.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(bool))]
    internal class EqualityConverter : IValueConverter
    {
        public static EqualityConverter Default { get; } = new EqualityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported on {nameof(EqualityConverter)}");
        }
    }
}