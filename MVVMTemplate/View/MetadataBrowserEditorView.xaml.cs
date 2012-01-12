using System;
using System.Collections.Generic;
using System.Linq;
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
using ecologylab.semantics.metametadata;
using MVVMTemplate.ViewModel;
using Simpl.Serialization;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for MetadataBrowserEditorView.xaml
    /// </summary>
    public partial class MetadataBrowserEditorView : UserControl
    {
        public MetadataBrowserEditorView(Metadata metadata)
        {
            InitializeComponent();

            this.DataContext = new MetadataBrowserEditorViewModel(metadata);

            this.BuildFields();
        }

        private void BuildFields()
        {
            MetadataBrowserEditorViewModel viewModel            = (MetadataBrowserEditorViewModel) this.DataContext;
            MetaMetadataOneLevelNestingEnumerator enumerator    = viewModel.DisplayedFields;

             while( enumerator.MoveNext())
             {  
                 MetaMetadataField mmdField     = enumerator.Current;
                 MetaMetadata currentMM         = (MetaMetadata) enumerator.CurrentMetadata.MetaMetadata;
                 
                 if (currentMM.IsChildFieldDisplayed(mmdField.Name))
                 {
                     switch (mmdField.GetFieldType())
                     {
                         case FieldTypes.Scalar:
                             this.FieldsRoot.Children.Add(new MetadataScalarFieldTextView(
                                                              (MetaMetadataScalarField) mmdField,
                                                              enumerator.CurrentMetadata));
                             break;
                         case FieldTypes.CompositeElement:
                             break;
                         case FieldTypes.CollectionElement:
                         case FieldTypes.CollectionScalar:
                             break;
                     }
                 }
             }
        }
    }
}
