using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Context;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;

namespace ecologylab.semantics.metadata
{
    public class MetadataDeserializationHookStrategy : IDeserializationHookStrategy
    {

        Stack<MetaMetadataNestedField> _currentMMStack = new Stack<MetaMetadataNestedField>();
        private SemanticsSessionScope _semanticsSessionScope;
        MetaMetadata _metaMetadata;

        public MetadataDeserializationHookStrategy(SemanticsSessionScope semanticsSessionScope)
        {
            _semanticsSessionScope = semanticsSessionScope;
        }
        
        public void DeserializationPreHook(Object e, FieldDescriptor fd)
        {
            var deserializedMetadata = e as Metadata;
            if (deserializedMetadata == null) return;

            if (_currentMMStack.Count == 0)
            {
                MetaMetadataCompositeField deserializationMM	= deserializedMetadata.MetaMetadata;
//					MetaMetadataCompositeField metaMetadata				= semanticsSessionScope.getMetaMetadataRepository().getByClass(InformationComposition.class);
//					if (metaMetadata != null && metaMetadata.bindMetaMetadataToMetadata(deserializationMM))
//					{
//						metaMetadata = (MetaMetadata) deserializationMM;
//					}
//					else
//					{
//						deserializedMetadata.setMetaMetadata(metaMetadata);
//					}
                _currentMMStack.Push(deserializationMM);
            }
            else if (fd is MetadataFieldDescriptor)
            {
                MetadataFieldDescriptor mfd 				= (MetadataFieldDescriptor) fd;
                String mmName								= mfd.MmName;
                MetaMetadataNestedField currentMM			= _currentMMStack.Peek();
                MetaMetadataNestedField childMMNested		= (MetaMetadataNestedField) currentMM.LookupChild(mmName);
                MetaMetadataCompositeField childMMComposite = null;
                if (childMMNested.IsPolymorphicInherently)
                {
                    String tagName = deserializedMetadata.ClassDescriptor.TagName;
                    childMMComposite	= _semanticsSessionScope.MetaMetadataRepository.GetMMByName(tagName);
                }
                else
                {
                    childMMComposite = childMMNested.GetMetaMetadataCompositeField();
                }
                deserializedMetadata.MetaMetadata = childMMComposite;
                _currentMMStack.Push(childMMComposite);
            }
				
            if (e is Document)
                ((Document) e).SemanticsSessionScope = _semanticsSessionScope;
        }

        public void DeserializationPostHook(Object e, FieldDescriptor fd)
        {
            if (!(e is Metadata)) return;

            if (fd != null && !(fd is MetadataFieldDescriptor))
                Console.WriteLine("deserializationPostHook(): call with non-metadata field descriptor! probably this is a mistake!");
            else
                _currentMMStack.Pop();
        }
    }
}
