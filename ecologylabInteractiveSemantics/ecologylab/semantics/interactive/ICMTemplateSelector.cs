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

            FieldEntry items;
            bool inList = false;
            object theField = null;
            if (item is FieldEntry)
            {
                items = (FieldEntry)item;
                theField = items.Value;
            }
            else
            {
                theField = item;
                inList = true; //We encounter items directly only if they exist in lists.
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
            /*else if (theField is Entity)
            { //EntityListItemDataTemplate
                string key = inList ? "EntityListItemDataTemplate" : "EntityDataTemplate";

                dt = pres.FindResource(key) as DataTemplate;
                Console.WriteLine("Template [" + theField + "] : " + key);
            }*/
            else if (theField is Metadata)
            {
                Type t = theField.GetType();
                string customKey = t.Name + "DataTemplate";
                object ob = pres.TryFindResource(customKey);
                if (ob != null)
                {
                    Console.WriteLine("Using custom dataTemplate: " + customKey);
                    dt = ob as DataTemplate;
                }
                
                key = inList ? "CompositeListItemDataTemplate" : "CompositeDataTemplate";
            }
            else
            {
                Console.WriteLine("Template not found for object: " + item);
            }

            if (dt == null && key != null)
            {
                dt = pres.FindResource(key) as DataTemplate;
                //Console.WriteLine("Template [" + theField + "] : " + key);
            }
            return dt;
        }
    }
}
