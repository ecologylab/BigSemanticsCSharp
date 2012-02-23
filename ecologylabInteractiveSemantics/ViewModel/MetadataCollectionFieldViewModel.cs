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
    public class MetadataCollectionFieldViewModel : MetadataNestedFieldViewModel<MetaMetadataCollectionField>
    {
        public MetadataCollectionFieldViewModel(MetaMetadataCollectionField metaMetadataField, Metadata metadata, int nestedLevel) : base(metaMetadataField, metadata, nestedLevel)
        {
        }

        public override bool MultipleVisibleFields
        {
            get { return ((ICollection) this.FieldValue).Count > 1; }
        }
    }
}
