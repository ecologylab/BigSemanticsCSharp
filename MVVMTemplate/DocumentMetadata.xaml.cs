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
using ecologylab.semantics.metadata.builtins;
using Simpl.Serialization;

namespace MVVMTemplate
{
	/// <summary>
	/// Interaction logic for DocumentMetadata.xaml
	/// </summary>
	public partial class DocumentMetadata : UserControl
	{
        public List<MetadataField> MetadataFields { get; set; }

        public DocumentMetadata(Document parsedDoc)
        {
            this.InitializeComponent();

            MetadataFields = new List<MetadataField>();

            foreach (FieldDescriptor descriptor in parsedDoc.ClassDescriptor.GetAllFields())
            {
                MetadataField fieldView = new MetadataField(descriptor, parsedDoc);
                MetadataFields.Add(fieldView);
                MainStack.Children.Add(fieldView);
            }
        }
	}
}