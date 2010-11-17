using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ecologylab.semantics.metametadata
{
    public class RepositoryPatternEntry
    {
        private Regex           pattern;
        private MetaMetadata    metaMetadata;

        public RepositoryPatternEntry(Regex pattern, MetaMetadata metaMetadata)
        {
            this.pattern        = pattern;
            this.metaMetadata   = metaMetadata;
        }

        public Regex Pattern
        {
            get { return pattern; }
        }

        public MetaMetadata MetaMetadata
        {
            get { return metaMetadata; }
        }
    }
}
