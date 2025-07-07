using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using Svg.Pathing;

// Расширение для простого преобразования цветов
public static class ColorExtensions
{
    public static System.Drawing.Color ToSystemDrawingColor(this Color mediaColor)
    {
        return System.Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
    }
}

// Класс для преобразования PathGeometry в строку для SVG
public static class SvgPathBuilder
{
    public static SvgPathSegmentList FromPathGeometry(System.Windows.Media.PathGeometry geometry)
    {
        // 1. Получаем строку PathData из WPF PathGeometry
        string pathDataString = geometry.ToString(CultureInfo.InvariantCulture);

        // 2. Используем TypeDescriptor для преобразования строки в SvgPathSegmentList
        var converter = TypeDescriptor.GetConverter(typeof(SvgPathSegmentList));
        if (converter == null || !converter.CanConvertFrom(typeof(string)))
            throw new InvalidOperationException("SvgPathSegmentList converter not available.");

        var segmentList = (SvgPathSegmentList)converter.ConvertFromInvariantString(pathDataString);

        return segmentList;
    }
}