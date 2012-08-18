using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions;
using ecologylab.semantics.documentparsers;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    [SimplInherit]
    [SimplTag("reselect_meta_metadata_and_extract")]
    public class ReselectAndExtractMetadataSemanticOperation : SemanticOperation
    {

        public override string GetOperationName()
        {
            return SemanticOperationStandardMethods.ReselectMetametadataAndExtract;
        }

        public override void HandleError()
        {
        }

        public override object Perform(object obj)// throws IOException
        {
            CompoundDocument doc = (CompoundDocument) obj;
            MetaMetadata mmd = (MetaMetadata) doc.MetaMetadata;

            return null;
        }
    }
}
