using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    class MetadataCompositeFieldViewModel : MetadataNestedFieldViewModel<MetaMetadataCompositeField>
    {
        public MetadataCompositeFieldViewModel(MetaMetadataCompositeField metaMetadataField, Metadata metadata) : base(metaMetadataField, metadata)
        {
        }
    }
}
