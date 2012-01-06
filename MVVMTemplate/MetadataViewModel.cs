using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate
{
    public class MetadataViewModel : IEnumerable<MetaMetadataField>
    {
        private Metadata _metadata;

        public IEnumerator<MetaMetadataField> DisplayedFields
        {
            get { return Metadata.MetaMetadataIterator(); }
        }

        public Metadata Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        public MetadataViewModel(Metadata metadata)
        {
            Metadata = metadata;

        }


        public IEnumerator<MetaMetadataField> GetEnumerator()
        {
            return DisplayedFields;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
