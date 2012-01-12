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
    /// Interaction logic for MetadataFieldsGroupView.xaml
    /// </summary>
    public partial class MetadataFieldsGroupView : UserControl
    {
        public MetadataFieldsGroupView()
        {
            InitializeComponent();

//            this.Loaded += new RoutedEventHandler(MetadataFieldsGroupView_Loaded);
        }

//        void MetadataFieldsGroupView_Loaded(object sender, RoutedEventArgs e)
//        {
//
//            this.DataContext = new MetadataViewModelBase(this.Metadata);
//            this.BuildFields(this.Metadata.MetaMetadataIterator());
//        }

        public void BuildFields(MetaMetadataOneLevelNestingEnumerator enumerator)
        {
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
                              this.FieldsRoot.Children.Add(new MetadataCompositeFieldView(
                                                              (MetaMetadataCompositeField) mmdField,
                                                              enumerator.CurrentMetadata));
                             break;
                         case FieldTypes.CollectionElement:
                         case FieldTypes.CollectionScalar:
//                            this.FieldsRoot.Children.Add(new MetadataCollectionFieldView(
//                                                              (MetaMetadataCollectionField) mmdField,
//                                                              enumerator.CurrentMetadata));
                            break;
                     }
                 }
             }
        }

        public static readonly DependencyProperty MetadataProperty = DependencyProperty.Register(
            "Metadata", 
            typeof(Metadata), 
            typeof(MetadataFieldsGroupView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnMetadataChanged)));

        private static void OnMetadataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Metadata newValue = (Metadata) e.NewValue;
            ((MetadataFieldsGroupView) d).BuildFields(newValue.MetaMetadataIterator());
        }

        public Metadata Metadata
        {
            get { return (Metadata) GetValue(MetadataProperty); } 
            set
            {
                SetValue(MetadataProperty, value);
                BuildFields(value.MetaMetadataIterator());
            }
        }

        
    }
}
