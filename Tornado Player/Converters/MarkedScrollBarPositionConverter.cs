namespace Tornado.Player.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    [ValueConversion(typeof(object[]), typeof(double))]
    internal class MarkedScrollBarPositionConverter : IMultiValueConverter
    {
        public static MarkedScrollBarPositionConverter Default { get; } = new MarkedScrollBarPositionConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ScrollBar scrollBar = (ScrollBar)values[0];
            double scrollBarHeight = (double)values[1];
            double lineHeight = (double)values[2];
            int selectedIndex = (int)values[3];
            int totalItems = (int)values[4];

            double topButtonOffset = ((RepeatButton)scrollBar.Template.FindName("PART_LineUpButton", scrollBar)).ActualHeight;
            double bottomButtonOffset = ((RepeatButton)scrollBar.Template.FindName("PART_LineDownButton", scrollBar)).ActualHeight;

            double totalScrollSpace = scrollBarHeight - (topButtonOffset + bottomButtonOffset);

            double linePositionIncrement = totalScrollSpace / totalItems;

            double lineScrollSpacePosition = linePositionIncrement * selectedIndex;

            double absoluteLinePosition = lineScrollSpacePosition + topButtonOffset;

            return absoluteLinePosition - (lineHeight / 2.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported on {nameof(MarkedScrollBarPositionConverter)}.");
        }
    }
}