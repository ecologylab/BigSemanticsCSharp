using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ecologylab.semantics.metadata.scalar;
using System.Collections;
using ecologylab.semantics.metadata;
using ecologylab.serialization;

namespace ecologylab.semantics.interactive
{
    public class ICMTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item == null)
                return null;

            object[] items = item as object[];

            object theField = items == null ? item : items[1];

            Window pres = Application.Current.MainWindow;

            DataTemplate dt = null;

            //This might be replaced by type specific run-time generated datatemplates.
            //This code is probably not optimal for performance. Not concerned at this point.
            if (theField is IMetadataScalar)
            {
                dt = pres.FindResource("MetadataScalarDataTemplate") as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : Scalar");
            }
            else if (theField is IEnumerable)
            {
                dt = pres.FindResource("MetadataListDataTemplate") as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : List");
            }
            else if (theField is Metadata)
            {
                dt = pres.FindResource("MetadataDataTemplate") as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : Composite");
            }
            else
            {
                Console.WriteLine("Template not found for object: " + item);
            }

            return dt;
        }
    }
}
