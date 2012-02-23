using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    public abstract class MetadataNestedFieldViewModel<TM> : MetadataFieldViewModel<TM> where TM : MetaMetadataNestedField
    {
        public MetadataNestedFieldViewModel(TM metaMetadataField, Metadata metadata, int nestedLevel) : base(metaMetadataField, metadata)
        {
            NestedLevel = nestedLevel;
        }

        protected override Binding CreateBinding(Metadata metadata, string mmdFieldName)
        {
            return new Binding
                {
                    Source = metadata,
                    Path = new PropertyPath(mmdFieldName),
                };
        }

        public static readonly DependencyProperty NestedLevelProperty = DependencyProperty.Register("NestedLevel", typeof(int), typeof(MetadataFieldViewModel<TM>));

        public int NestedLevel
        {
            get { return (int) GetValue(NestedLevelProperty); }
            set { SetValue(NestedLevelProperty, value); }
        }
    }
}
