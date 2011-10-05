using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Generic;

using ecologylab.semantics.metadata;

namespace ecologylab.semantics.metametadata
{
    public class MetaMetadataOneLevelNestingEnumerator : OneLevelNestingEnumerator<MetaMetadataField, MetaMetadataField>
    {
        private IEnumerator<Metadata> _nextMetadatas 	= null;
	
	    private Metadata _currentMetadata 				= null;
	
	    public MetaMetadataOneLevelNestingEnumerator(MetaMetadataField firstObject, Metadata firstMetadata, List<Metadata> mixinMetadatas) : base(firstObject, createMixinCollectionIterator(mixinMetadatas))
        {
		    _currentMetadata = firstMetadata;
		    if (mixinMetadatas != null)
			    _nextMetadatas = mixinMetadatas.GetEnumerator();
	    }

	    public bool MoveNext() 
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
	
	    public Metadata CurrentMetadata
	    {
            get { return _currentMetadata; }
	    }
	
	    private static IEnumerator<MetaMetadataField> createMixinCollectionIterator(List<Metadata> mixinMetadatas)
	    {
		    List<MetaMetadataCompositeField> mixinMetaMetadatas = null;
		    if (mixinMetadatas != null)
		    {
			    mixinMetaMetadatas = new List<MetaMetadataCompositeField>();
			    foreach (Metadata metadata in mixinMetadatas)
				    mixinMetaMetadatas.Add(metadata.MetaMetadata);
			
			    return mixinMetaMetadatas.GetEnumerator();
		    }
		    else
			    return null;
		
	    }
    }
}
