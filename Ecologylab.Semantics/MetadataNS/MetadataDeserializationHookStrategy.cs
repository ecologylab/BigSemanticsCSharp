using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Simpl.Serialization.Context;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetadataNS
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
                    String tagName = deserializedMetadata.MetadataClassDescriptor.TagName;
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
                Debug.WriteLine("deserializationPostHook(): call with non-metadata field descriptor! probably this is a mistake!");
            else
                _currentMMStack.Pop();
        }
    }
}
