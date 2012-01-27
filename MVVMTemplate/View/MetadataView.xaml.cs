using System;
using System.Collections;
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
    /// Interaction logic for MetadataView.xaml
    /// </summary>
    public partial class MetadataView : UserControl, IExpandable
    {
        public MetadataView()
        {
            InitializeComponent();
        }

        public MetadataView(Metadata metadata)
        {
            InitializeComponent();

            this.Metadata = metadata;
        }

        public void BuildFields(Metadata fieldValue)
        {
            BuildFields(fieldValue.MetaMetadataIterator());
        }

        public void BuildFields(MetaMetadataOneLevelNestingEnumerator enumerator)
        {
           while (enumerator.MoveNext() && (this.IsExpanded || this.FieldsRoot.Children.Count == 0))
            {
                MetaMetadataField mmdField = enumerator.Current;
                MetaMetadataCompositeField currentMM = enumerator.CurrentMetadata.MetaMetadata;
                object metadataValue = (mmdField.MetadataFieldDescriptor != null) ? mmdField.MetadataFieldDescriptor.GetObject(enumerator.CurrentMetadata) : null;

                if (currentMM.IsChildFieldDisplayed(mmdField.Name) && metadataValue != null)
                {
                    AddField(mmdField, enumerator.CurrentMetadata, metadataValue);
                }
            }
        }

       private void AddField(MetaMetadataField mmdField, Metadata metadata, object metadataValue)
        {
            switch (mmdField.GetFieldType())
            {
                case FieldTypes.Scalar:
                    this.FieldsRoot.Children.Add(new MetadataScalarFieldTextView(
                                                        (MetaMetadataScalarField) mmdField,
                                                        metadata));
                    break;
                case FieldTypes.CompositeElement:
                    this.FieldsRoot.Children.Add(new MetadataCompositeFieldView(
                                                        (MetaMetadataCompositeField) mmdField,
                                                        metadata));
                    break;
                case FieldTypes.CollectionElement:
                case FieldTypes.CollectionScalar:
                    if(((ICollection) metadataValue).Count > 0)
                        this.FieldsRoot.Children.Add(new MetadataCollectionFieldView(
                                                            (MetaMetadataCollectionField) mmdField,
                                                            metadata));
                    break;
            }
        }

        public static readonly DependencyProperty MetadataProperty = DependencyProperty.Register(
            "Metadata", 
            typeof(Metadata), 
            typeof(MetadataView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnMetadataChanged)));

        private static void OnMetadataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ((MetadataView) d).BuildFields((Metadata) e.NewValue);
        }

        public Metadata Metadata
        {
            get { return (Metadata) GetValue(MetadataProperty); } 
            set
            {
                SetValue(MetadataProperty, value);
                BuildFields(value);
            }
        }

        public void Expand()
        {
            this.IsExpanded = true;
            MetaMetadataOneLevelNestingEnumerator enumerator = Metadata.MetaMetadataIterator();
            enumerator.MoveNext();
            this.BuildFields(enumerator);
        }

        public void Collapse()
        {
            this.IsExpanded = false;
            for (int i=FieldsRoot.Children.Count-1; i > 0; i--)
            {
                this.FieldsRoot.Children.RemoveAt(i);
            }
        }

        public void ToggleExpand()
        {
            if (this.IsExpanded)
                Collapse();
            else
                Expand();
        }

        private UIElement _expandAffordance;
        public UIElement ExpandAffordance
        {
            get { return _expandAffordance; }
            set { _expandAffordance = value; }
        }

        public bool IsExpanded { get; set; }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ToggleExpand();
        }
    }
}
