using System;
using System.Collections.Generic;
using System.Reflection;
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
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;

namespace MVVMTemplate
{
	/// <summary>
	/// Interaction logic for DocumentMetadata.xaml
	/// </summary>
	public partial class TemplatedMetadataBrowserEditorView : UserControl
	{
        public List<MetadataField> MetadataFields { get; set; }

        public TemplatedMetadataBrowserEditorView(Document parsedDoc)
        {
            this.InitializeComponent();

            this.Loaded += new RoutedEventHandler(MetadataBrowserEditorView_Loaded);
            
        }

	    public void MetadataBrowserEditorView_Loaded(object sender, RoutedEventArgs e)
	    {
	        Console.WriteLine("Loaded the View, rebinding values");

	        var context = (MetadataViewModel) MyItemsControl.ItemsSource;
	        Metadata metadata = context.Metadata;
	        int index = 0;
	        var metaMetadataOneLevelNestingEnumerator = metadata.MetaMetadataIterator();

            while(metaMetadataOneLevelNestingEnumerator.HasNext())
            {
                metaMetadataOneLevelNestingEnumerator.MoveNext();
                MetaMetadataField mmdField = metaMetadataOneLevelNestingEnumerator.Current;
                string mmdFieldName = mmdField.Name;
                var contentPresenter = (ContentPresenter) MyItemsControl.ItemContainerGenerator.ContainerFromIndex(index++);

                StackPanel panel = (StackPanel) contentPresenter.ContentTemplate.FindName("itemStackPanel", contentPresenter);

                TextBox box = (TextBox) panel.Children[1];
                if (box == null)
                {
                    Console.WriteLine("Fail. No box found");
                    break;
                }
                mmdFieldName = char.ToUpper(mmdFieldName[0]) + mmdFieldName.Substring(1);
                Console.WriteLine("Binding: " + mmdFieldName);
                Binding b = new Binding
                {
                    Source = metadata,
                    Path = new PropertyPath(mmdFieldName + ".Value"),
                };

                FieldInfo fInfo = metadata.GetType().GetField(mmdFieldName);
                BindingOperations.SetBinding( box, TextBox.TextProperty, b);
                
            }

	    }
	}
}