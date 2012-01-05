using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;

namespace MVVMTemplate
{
    class FieldViewModel
    {
        public String Name { get; set; }

        public String Value { get; set; }

        public FieldViewModel(FieldDescriptor descriptor)
        {
            Name = descriptor.Name;

            //TODO wrong
            Value = descriptor.ToString();
            /*
            fieldName.Text = descriptor.Name;
            if (descriptor.Field != null)
            {
                object value = descriptor.GetObject(parsedDoc);
                if (value is MetadataString)
                {
                    MetadataString mdString = (MetadataString)value;
                    fieldValue.Text = mdString.Value;
                }
                else if (value != null)
                    fieldValue.Text = value.GetType().ToString();
                else
                    fieldValue.Text = "null";
            }
             * */
        }
    }
}
