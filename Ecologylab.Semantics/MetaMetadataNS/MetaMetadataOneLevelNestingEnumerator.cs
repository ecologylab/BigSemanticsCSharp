using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS;
using Simpl.Fundamental.Generic;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class MetaMetadataOneLevelNestingEnumerator : OneLevelNestingEnumerator<MetaMetadataField, MetaMetadataField>
    {
        private readonly IEnumerator<MetadataNS.Metadata> _nextMetadatas 	= null;
	
	    private MetadataNS.Metadata _currentMetadata 				        = null;
	
	    public MetaMetadataOneLevelNestingEnumerator(MetaMetadataField firstObject, MetadataNS.Metadata firstMetadata) : base(firstObject, CreateMixinCollectionIterator(firstMetadata.Mixins))
        {
            _currentMetadata = firstMetadata;
            if (firstMetadata.Mixins != null)
		        _nextMetadatas = firstMetadata.Mixins.GetEnumerator();
	    }

	    public override bool MoveNext() 
	    {
            if (base.MoveNext())
                return true;

            bool newMetadata = (_currentIterator == null && _firstIterator.Current == null) || 
			    (_currentIterator != null && _currentIterator.Current == null); // && (currentIterator == firstIterator ) || ();
            if (newMetadata && _nextMetadatas != null)
            {
                _nextMetadatas.MoveNext();
                _currentMetadata = _nextMetadatas.Current;
                return true;
            }
            
		    return false;
	    }
	
	    public MetadataNS.Metadata CurrentMetadata
	    {
            get { return _currentMetadata; }
	    }
	
	    private static IEnumerator<MetaMetadataField> CreateMixinCollectionIterator(IEnumerable<MetadataNS.Metadata> mixinMetadatas)
	    {
		    List<MetaMetadataCompositeField> mixinMetaMetadatas = null;
		    if (mixinMetadatas != null)
		    {
			    mixinMetaMetadatas = new List<MetaMetadataCompositeField>();
			    foreach (MetadataNS.Metadata metadata in mixinMetadatas)
				    mixinMetaMetadatas.Add(metadata.MetaMetadata);
			
			    return mixinMetaMetadatas.GetEnumerator();
		    }
		    else
			    return null;
		
	    }
    }
}
