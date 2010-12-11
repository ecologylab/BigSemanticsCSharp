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
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.generated.library;

namespace ecologylab.semantics.interactive
{
    public class ICMTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item == null)
                return null;

            object[] items = item as object[];

            bool inList = false;
            object theField = null;
            if (items == null)
                theField = item;
            else
            {
                theField = items[1];
                inList = true;
            }


            Window pres = Application.Current.MainWindow;

            DataTemplate dt = null;

            String key = null;
            //This might be replaced by type specific run-time generated datatemplates.
            //This code is probably not optimal for performance. Not concerned at this point.
            if (theField is IMetadataScalar)
            {
                key = "ScalarDataTemplate";
            }
            else if (theField is IEnumerable)
            {
                key = "ListDataTemplate";
            }
            else if (theField is Thumbinner)
            {
                key = "ImageDataTemplate";
            }
            /*else if (theField is Entity)
            { //EntityListItemDataTemplate
                string key = inList ? "EntityListItemDataTemplate" : "EntityDataTemplate";

                dt = pres.FindResource(key) as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : " + key);
            }*/
            else if (theField is Metadata)
            {
                key = inList ? "CompositeListItemDataTemplate" : "CompositeDataTemplate";
            }
            else
            {
                Console.WriteLine("Template not found for object: " + item);
            }

            if (key != null)
            {
                dt = pres.FindResource(key) as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : " + key);
            }
            return dt;
        }
    }
}
