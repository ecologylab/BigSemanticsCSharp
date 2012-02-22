using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ecologylab.interactive.Utils
{

    #region Convertors

    [ValueConversion(typeof(double), typeof(double))]
    public class ScalingConverter : IValueConverter
    {
        private double scale;

        public ScalingConverter(double scale)
        {
            this.scale = scale;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double curValue = (double)value;
            return Math.Round(curValue, 2) * scale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }


    [ValueConversion(typeof(double), typeof(double))]
    public class TwoDecimalPointRoundingConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double curValue = (double)value;
            return Math.Round(curValue, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class SnappingRange_Negative1To1Converter : IValueConverter
    {
        private const int steps = 10;
        private const double range = 2;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double curValue = (double)value;
            return ((int)(curValue * steps / range + .5) * range / steps);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class SnappingContrastRangeConverter : IValueConverter
    {
        double range = 8;
        int steps = 10;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double curValue = (double)value;
            return ((int)(curValue * steps / range + .5) * range / steps);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class SnappingOpacityRangeConverter : IValueConverter
    {
        double range = 1;
        int steps = 10;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double curValue = (double)value;
            return ((int)(curValue * steps / range + .5) * range / steps);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// Use this to form a clip on a canvas based on an existing path.
    /// It uses the data of the incoming path, intersecting with the containing canvas, this can be used as a clip.
    /// The returned path's origin will be the origin of the containing canvas.
    /// </summary>
    [ValueConversion(typeof(Path), typeof(StreamGeometry))]
    public class InvertPathConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Path path = value as Path;
            if (path == null) throw new InvalidOperationException("InvertPathConverter requires a Path as an element binding input");

            Canvas canvas = path.Parent as Canvas;
            if (canvas == null) throw new InvalidOperationException("InvertPathConverter requires the Path input to be contained within a canvas");

            //Note: the path shouldn't have stretch="Fill"!
            Point pathToClipAt = new Point(Canvas.GetLeft(path), Canvas.GetTop(path));
            
            //Get the pathGeometry, and make it's origin the same as the canvas.
            Geometry pathToClip = Geometry.Combine(path.Data, Geometry.Empty, GeometryCombineMode.Union, new TranslateTransform(pathToClipAt.X, pathToClipAt.Y));
            //Since the containing rect is positioned at canvas origin, no need to translate
            Rect containingRect = new Rect {Height = canvas.Height, Width = canvas.Width};
            RectangleGeometry containingRectGeom = new RectangleGeometry(){Rect = containingRect};

            return Geometry.Combine(containingRectGeom, pathToClip, GeometryCombineMode.Exclude, Transform.Identity);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    #endregion

}
