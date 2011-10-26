using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.collecting
{
    public class SemanticsGlobalScope : MetaMetadataRepositoryInit
    {
        public SemanticsGlobalScope(SimplTypesScope metadataTranslationScope, string repoLocation) : base(metadataTranslationScope, repoLocation)
        {
        }
    }
}
