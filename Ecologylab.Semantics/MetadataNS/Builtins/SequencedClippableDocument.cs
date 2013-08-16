using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class SequencedClippableDocument : SequencedClippableDocumentDeclaration
    {
        public SequencedClippableDocument()
		{ }

        public SequencedClippableDocument(MetaMetadataCompositeField mmd) : base(mmd) 
        { }
    }
}
