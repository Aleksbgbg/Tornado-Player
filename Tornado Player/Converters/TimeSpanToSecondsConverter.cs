namespace Tornado.Player.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(TimeSpan), typeof(double))]
    internal class TimeSpanToSecondsConverter : IValueConverter
    {
        public static TimeSpanToSecondsConverter Default { get; } = new TimeSpanToSecondsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TimeSpan)value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }
}