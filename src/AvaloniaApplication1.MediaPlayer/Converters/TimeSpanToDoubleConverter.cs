using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AvaloniaApplication1.MediaPlayer.Converters
{
    public class TimeSpanToDoubleConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var valueAdd = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return ((TimeSpan?)value)?.TotalSeconds + valueAdd ?? 0.0;
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var valueAdd = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return value != null ? TimeSpan.FromSeconds((double)value - valueAdd) : TimeSpan.Zero;
        }
    }
}
