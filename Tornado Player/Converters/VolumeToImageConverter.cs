namespace Tornado.Player.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    [ValueConversion(typeof(double), typeof(ImageSource))]
    internal class VolumeToImageConverter : IValueConverter
    {
        public static VolumeToImageConverter Default { get; } = new VolumeToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double volume = (double)value;

            if (volume == 1.0)
            {
                return Application.Current.FindResource("MaxVolume");
            }

            if (volume >= 0.5)
            {
                return Application.Current.FindResource("HighVolume");
            }

            if (volume > 0.0)
            {
                return Application.Current.FindResource("LowVolume");
            }

            return Application.Current.FindResource("Mute");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported on {nameof(VolumeToImageConverter)}");
        }
    }
}