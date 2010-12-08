using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ecologylab.serialization;
using ecologylab.semantics.metadata;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using System.Globalization;
using ecologylab.semantics.metadata.scalar;

namespace ecologylab.semantics.interactive
{
    /// <summary>
    /// Helper class that build UI elements for InContextMetadata
    /// </summary>
    public static class ICMBuilder
    {

        /// <summary>
        /// Recursive call to build a hierarchical tree view for metadata
        /// </summary>
        /// <param name="fd"></param>
        /// <returns></returns>
        public static FrameworkElement BindMetadata(Metadata metadata, FieldDescriptor fd)
        {
            FrameworkElement item = null;

            if (fd.Field == null) 
                fd = fd.WrappedFieldDescriptor;

            String name = fd.FieldName;

            object value = fd.Field == null ? null : fd.Field.GetValue(metadata);
            //Console.Write("\nField: " + name + "\t\t -\t" + value ?? "null");

            if (value == null)
            {
                //Console.WriteLine(" -- Skipped");
                return null;
            }
            //else
                //Console.WriteLine("");

            //Use DataTemplate to Structure elements.
            //Keep design AWAY from functionality.

            switch (fd.Type)
            {
                case FieldTypes.SCALAR:
                    Console.WriteLine("Scalar [" + name + "]: " + value);
                   
                    break;
                case FieldTypes.COMPOSITE_ELEMENT:
                    Console.WriteLine("Composite [" + name + "] Type: " + fd.Field.FieldType);
                    Metadata compositeMetadata = (Metadata)value;
                    break;
                case FieldTypes.COLLECTION_SCALAR:
                    Console.WriteLine("Scalar Collection [" + name + "] Type: " + fd.Field.FieldType);
                    break;
                case FieldTypes.COLLECTION_ELEMENT:
                    Console.WriteLine("Element Collection [" + name + "] Type: " + fd.Field.FieldType);
                    break;
                default:
                    Console.WriteLine("Unhandled Type: " + fd.Type);
                    break;
            }


            return item;
        }

        public static IEnumerable MakeSource(Metadata metadata)
        {
            ClassDescriptor classDesc = metadata.ElementClassDescriptor;

            foreach (FieldDescriptor fd in classDesc.GetAllFields())
            {
                if (fd == null)
                    continue;


                FrameworkElement treeItem = BindMetadata(metadata, fd);
                if (treeItem == null)
                    continue;
                yield return treeItem;
            }

            yield break;
        }
    }

    [ValueConversion(typeof(FieldDescriptor), typeof(String))]
    public class MetadataFieldDescriptorNameConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FieldDescriptor fd = value as FieldDescriptor;
            if (fd == null)
                return "Not an fd";
            if (fd.WrappedFieldDescriptor != null)
                return fd.WrappedFieldDescriptor.FieldName;
            return fd.FieldName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(FieldDescriptor), typeof(String))]
    public class MetadataFieldDescriptorValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            
            FieldDescriptor fd = values[0] as FieldDescriptor;
            ContentControl parent = values[1] as ContentControl;

            object metadata = parent.Content as Metadata;

            if (fd.Field == null)
                fd = fd.WrappedFieldDescriptor;

            if (fd.Field == null)
                return null;


            switch(fd.Type)
            {
                case FieldTypes.SCALAR:
                    object val = fd.Field.GetValue(metadata);
                    return val != null ? val.ToString() : null;
                default:
                    Console.WriteLine("Other " + fd.FieldName);
                    break;
            }
            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
