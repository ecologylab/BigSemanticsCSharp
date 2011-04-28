using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace DomExtraction
{
    public class LevelConverter : DependencyObject, IMultiValueConverter
    {
        public object Convert(
            object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                Console.WriteLine("Unset Value !!");
                return (double)10;
            }
            TreeViewItem parentItem = (TreeViewItem)values[0];
            FrameworkElement curItem = parentItem;
            int level = 0;
            //NASTY lookup for level.
            while (curItem.Parent != null)
            {
                curItem = (FrameworkElement) curItem.Parent;
                if (curItem is TreeViewItem)
                    level++;
            }

            double indent = (double)values[1];
            return indent * level;
        }

        public object[] ConvertBack(
            object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
