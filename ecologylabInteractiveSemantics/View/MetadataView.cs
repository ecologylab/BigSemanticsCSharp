using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;
using MVVMTemplate.ViewModel;
using Simpl.Serialization;

namespace MVVMTemplate.View
{
    public class MetadataView : ExpandableViewBase
    {
        private MetaMetadataOneLevelNestingEnumerator _currentEnumerator;

        public MetadataView()
        {
            InitializeComponent();
        }

        public MetadataView(Metadata metadata)
        {
            InitializeComponent();

            this.Metadata = metadata;
        }

        public override void AddItem(object item)
        {
            MetaMetadataOneLevelNestingEnumerator enumerator    = Enumerator as MetaMetadataOneLevelNestingEnumerator;
            MetaMetadataCompositeField currentMM                = enumerator.CurrentMetadata.MetaMetadata;
            MetaMetadataField mmdField                          = item as MetaMetadataField;
            object metadataValue = (mmdField.MetadataFieldDescriptor != null) ? mmdField.MetadataFieldDescriptor.GetObject(enumerator.CurrentMetadata) : null;

            if (currentMM.IsChildFieldDisplayed(mmdField.Name) && metadataValue != null)
            {
                AddField(mmdField, enumerator.CurrentMetadata, metadataValue);
            }
        }

        private void AddField(MetaMetadataField mmdField, Metadata metadata, object metadataValue)
        {
            MetadataFieldViewBase field = null;
            int nestedLevel = (DataContext is MetadataCompositeFieldViewModel)
                                  ? ((MetadataCompositeFieldViewModel) DataContext).NestedLevel
                                  : 1;
            switch (mmdField.GetFieldType())
            {
                case FieldTypes.Scalar:
                    field = new MetadataScalarFieldTextView(
                                                        (MetaMetadataScalarField) mmdField,
                                                        metadata);
                    break;
                case FieldTypes.CompositeElement:
                    field = new MetadataCompositeFieldView(
                                                        (MetaMetadataCompositeField) mmdField,
                                                        metadata, nestedLevel+1);
                    break;
                case FieldTypes.CollectionElement:
                case FieldTypes.CollectionScalar:
                    if(((ICollection) metadataValue).Count > 0)
                        field = new MetadataCollectionFieldView(
                                                            (MetaMetadataCollectionField) mmdField,
                                                            metadata, nestedLevel+1);
                    break;
            }
            AddFieldAndAlignLabels(nestedLevel, field);

        }

        

        public static readonly DependencyProperty MetadataProperty = DependencyProperty.Register(
            "Metadata", 
            typeof(Metadata), 
            typeof(MetadataView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnMetadataChanged)));

        private static void OnMetadataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Metadata metadata       = e.NewValue as Metadata;
            MetadataView view       = d as MetadataView;
            if (metadata != null && view != null)
                view.Initialize(metadata.MetaMetadataIterator());
        }

        public Metadata Metadata
        {
            get { return (Metadata) GetValue(MetadataProperty); } 
            set
            {
                SetValue(MetadataProperty, value);
                Initialize(value.MetaMetadataIterator());
            }
        }

        public override IEnumerator Enumerator
        {
            get { return _currentEnumerator ?? (_currentEnumerator = this.Metadata.MetaMetadataIterator()); }
            set { this._currentEnumerator = value as MetaMetadataOneLevelNestingEnumerator; }
        }

        public override int ExpandableSize
        {
            get { throw new NotImplementedException(); }

        }
    }
}
