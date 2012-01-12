using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    class MetadataScalarFieldViewModel : MetadataFieldViewModel<MetaMetadataScalarField>
    {
        public MetadataScalarFieldViewModel(MetaMetadataScalarField metaMetadataField, Metadata metadata ) : base(metaMetadataField, metadata)
        {

        }
    }
}
