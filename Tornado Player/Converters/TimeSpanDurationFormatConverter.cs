namespace Tornado.Player.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(TimeSpan), typeof(string))]
    internal class TimeSpanDurationFormatConverter : IValueConverter
    {
        public static TimeSpanDurationFormatConverter Default { get; } = new TimeSpanDurationFormatConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan duration = (TimeSpan)value;

            return $"{(int)duration.TotalMinutes:00}:{duration.Seconds:00}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported {nameof(TimeSpanDurationFormatConverter)}.");
        }
    }
}