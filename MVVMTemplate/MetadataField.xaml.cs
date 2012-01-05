using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Simpl.Serialization;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;

namespace MVVMTemplate
{
	/// <summary>
	/// Interaction logic for MetadataField.xaml
	/// </summary>
	public partial class MetadataField : UserControl
	{
        public MetadataField(FieldDescriptor descriptor, Document parsedDoc)
        {
            InitializeComponent();
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
                    fieldValue.Text = value.ToString();
                else
                    fieldValue.Text = "null";
            }
        }
	}
}