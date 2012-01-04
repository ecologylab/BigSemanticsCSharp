//
//  Metadata.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/02/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.scalar;
using System.Collections;
using System.Windows.Controls;
using System.Windows;
using ecologylab.semantics.metametadata;
using ecologylab.net;


namespace ecologylab.semantics.metadata
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplDescriptorClasses(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	public class Metadata : NotificationObject
    {
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplOtherTags(new String[]{"meta_metadata_name", })]
		[SimplTag("mm_name")]
		private MetadataString metaMetadataName;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[semantics_mixin]
		[SimplCollection("mixins")]
		[MmName("mixins")]
		private List<Metadata> mixins;

        private MetaMetadataCompositeField metaMetadata;

        /**
	    * Hidden reference to the MetaMetadataRepository. DO NOT access this field directly. DO NOT
	    * create a static public accessor. -- andruid 10/7/09.
	    */
        private static MetaMetadataRepository repository;

		public Metadata()
		{ }

        public Metadata(MetaMetadataCompositeField metaMetadata) : this()
        {
            if (metaMetadata != null)
            {
                this.metaMetadata = metaMetadata;
                string metaMetadataName = metaMetadata.Name;
                if (ClassDescriptor.TagName != metaMetadataName)
                    this.metaMetadataName = new MetadataString(metaMetadataName);
            }
        }

        private ClassDescriptor classDescriptor;
        public ClassDescriptor ClassDescriptor
        {
            get
            {
                if (classDescriptor == null)
                    classDescriptor = ClassDescriptor.GetClassDescriptor(this);
                return classDescriptor;
            }
            private set { classDescriptor = value; }
        }

		public MetadataString MetaMetadataName
		{
			get { return metaMetadataName; }
			set { metaMetadataName = value; }
		}

		public List<Metadata> Mixins
		{
			get { return mixins; }
			set { mixins = value; }
		}

        public MetaMetadataCompositeField MetaMetadata
        {
            get { return metaMetadata ?? GetMetaMetadata(); }
            set { metaMetadata = value; }
        }

        public virtual MetadataParsedURL Location
        {
            get { return null; }
            set { }
        }

	    public virtual bool IsImage
	    {
	        get { return false; }
	        set { }
	    }

	    private MetaMetadataCompositeField GetMetaMetadata()
        {
            // return getMetadataClassDescriptor().getMetaMetadata();
            MetaMetadataCompositeField mm = metaMetadata;
            if (mm == null && repository != null)
            {
                if (metaMetadataName != null) // get from saved composition
                    mm = repository.GetMMByName(metaMetadataName.Value);

                if (mm == null)
                {
                    ParsedUri location = Location == null ? null : Location.Value;
                    if (location != null)
                    {
                        mm = IsImage ? repository.GetImageMM(location) : repository.GetDocumentMM(location);

                        // TODO -- also try to resolve by mime type ???
                    }
                    if (mm == null)
                        mm = repository.GetByClass(this.GetType());
                    if (mm == null && ClassDescriptor != null)
                    {
                        mm = repository.GetMMByName(ClassDescriptor.TagName);
                    }
                }
                if (mm != null)
                    MetaMetadata = mm;
            }
            return mm;
        }

        public MetaMetadataOneLevelNestingEnumerator MetaMetadataIterator(MetaMetadataField metaMetadataField)
        {
            MetaMetadataField firstMetaMetadataField = metaMetadataField ?? metaMetadata;
            return new MetaMetadataOneLevelNestingEnumerator(firstMetaMetadataField, this, mixins);
        }

        public void AddMixin(Metadata mixin)
        {
            if (Mixins == null)
            {
                Mixins = new List<Metadata>();
            }
            Mixins.Add(mixin);
        }
	}
}
