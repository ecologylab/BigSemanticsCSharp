using System;
using System.Collections;
using System.Linq;
using System.Text;
using Aga.Controls.Tree;
using ecologylab.serialization;

namespace ecologylab.semantics.metadata
{
    class MetadataModel : ITreeModel
    {

        public MetadataModel(Metadata metadata)
        {

        }


        public IEnumerable GetChildren(object parent)
        {
            var root = parent as Metadata;
            return root.EnumerableFields;
            //foreach (FieldEntry fEntry in root.EnumerableFields)
            //    yield return fEntry;
            
        }

        public bool HasChildren(object parent)
        {
            if (parent is Metadata)
                return true;
            else if (parent is FieldEntry)
            {

            }
            return false;
        }
    }
}
