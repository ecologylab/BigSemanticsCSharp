using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Context;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;

namespace ecologylab.semantics.metadata
{
    public class MetadataDeserializationHookStrategy : IDeserializationHookStrategy
    {
        public MetadataDeserializationHookStrategy()
        { }

        Stack<MetaMetadataNestedField> _currentMMstack = new Stack<MetaMetadataNestedField>();

        MetaMetadata _metaMetadata;

        /**
         * For the root, compare the meta-metadata from the binding with the one we started with. Down the
         * hierarchy, try to perform similar bindings.
         */
        public void deserializationPreHook(ElementState es, FieldDescriptor fd)
        {
            if ( es is Metadata)
            {
                Metadata deserializedMetadata = (Metadata)es;
                if (_currentMMstack.Count == 0)
                {
                    MetaMetadataCompositeField deserializationMM = deserializedMetadata.MetaMetadata;
                    _currentMMstack.Push(deserializationMM);
                }
                else
                {
                    String mmName = null;
                    if (fd is MetadataFieldDescriptor)
                    {
                        MetadataFieldDescriptor mfd = (MetadataFieldDescriptor)fd;
                        mmName = mfd.MmName;
                    }
                    else
                        mmName = "";//TODO FIXME XMLTools.GetXmlTagName(fd.FieldName, null);

                    MetaMetadataNestedField currentMM = _currentMMstack.Peek();
                    MetaMetadataNestedField childMMNested = (MetaMetadataNestedField)currentMM.LookupChild(mmName);
                    MetaMetadataCompositeField childMMComposite = childMMNested.GetMetaMetadataCompositeField();
                    deserializedMetadata.MetaMetadata = childMMComposite;
                    _currentMMstack.Push(childMMComposite);
                }
            }
        }

        public void deserializationPostHook(ElementState es, FieldDescriptor fd)
        {
            if (es is Metadata)
                _currentMMstack.Pop();
        }
//
//        private bool bindMetaMetadataToMetadata(MetaMetadataField deserializationMM, MetaMetadataField originalMM)
//        {
//            if (deserializationMM != null) // should be always
//            {
//                MetadataClassDescriptor originalClassDescriptor = originalMM.MetadataClassDescriptor;
//                MetadataClassDescriptor deserializationClassDescriptor = deserializationMM.MetadataClassDescriptor;
//
//                // quick fix for a NullPointerException for RSS. originalClassDescriptor can be null because
//                // it might be a meta-metadata that does not generate metadata class, e.g. xml
//                if (originalClassDescriptor == null)
//                    return true; // use the one from deserialization
//
//                bool sameMetadataSubclass = originalClassDescriptor.Equals(deserializationClassDescriptor);
//                // if they have the same metadataClassDescriptor, they can be of the same type, or one
//                // of them is using "type=" attribute.
//                bool useMmdFromDeserialization = sameMetadataSubclass
//                        && (deserializationMM.Type != null);
//                if (!useMmdFromDeserialization && !sameMetadataSubclass)
//                    // if they have different metadataClassDescriptor, need to choose the more specific one
//                    useMmdFromDeserialization = originalClassDescriptor.DescribedClass.IsAssignableFrom(
//                            deserializationClassDescriptor.DescribedClass);
//                return useMmdFromDeserialization;
//            }
//            else
//            {
//                System.Console.WriteLine("No meta-metadata in root after direct binding :-(");
//                return false;
//            }
//        }

        public void DeserializationPreHook(object o, FieldDescriptor fd)
        {
            throw new NotImplementedException();
        }

        public void DeserializationPostHook(object o, FieldDescriptor fd)
        {
            throw new NotImplementedException();
        }
    }
}
