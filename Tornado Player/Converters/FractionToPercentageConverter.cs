namespace Tornado.Player.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(double))]
    internal class FractionToPercentageConverter : IValueConverter
    {
        public static FractionToPercentageConverter Default { get; } = new FractionToPercentageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 100.0;
        }
    }
}