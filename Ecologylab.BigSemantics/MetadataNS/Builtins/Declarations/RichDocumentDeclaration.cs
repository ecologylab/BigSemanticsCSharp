//
// RichDocumentDeclaration.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator.
// Copyright 2015 Interface Ecology Lab. 
//


using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.MetadataNS.Scalar;
using Ecologylab.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations 
{
	[SimplInherit]
	public class RichDocumentDeclaration : Document
	{
		/// <summary>
		/// The Title of the Document
		/// </summary>
		[SimplScalar]
		[SimplHints(new Hint[] {Hint.XmlLeaf})]
		[SimplCompositeAsScalar]
		private MetadataString title;

		[SimplScalar]
		[SimplHints(new Hint[] {Hint.XmlLeaf})]
		private MetadataString description;

		/// <summary>
		/// Huamn readable name of the site.
		/// </summary>
		[SimplScalar]
		private MetadataString siteName;

		[SimplScalar]
		private MetadataString textKeywords;

		[SimplComposite]
		[MmName("see_also")]
		private RichDocument seeAlso;

		/// <summary>
		/// The search query
		/// </summary>
		[SimplScalar]
		[SimplHints(new Hint[] {Hint.XmlLeaf})]
		private MetadataString query;

		/// <summary>
		/// For debugging. Type of the structure recognized by information extraction.
		/// </summary>
		[SimplScalar]
		private MetadataString pageStructure;

		/// <summary>
		/// Clippings that this document contains.
		/// </summary>
		[SimplCollection]
		[SimplScope("repository_clippings")]
		[MmName("clippings")]
		private List<IClipping<Metadata>> clippings;

		[SimplComposite]
		[MmName("favicon")]
		private Image favicon;

		[SimplComposite]
		[MmName("thumbnail")]
		private Image thumbnail;

		[SimplCollection("image")]
		[MmName("main_images")]
		private List<Image> mainImages;

		[SimplCollection("video")]
		[MmName("main_videos")]
		private List<Video> mainVideos;

		[SimplCollection("audio")]
		[MmName("main_audio")]
		private List<Audio> mainAudio;

		[SimplCollection("article_bodie")]
		[MmName("article_bodies")]
		private List<Ecologylab.BigSemantics.MetadataNS.Scalar.MetadataString> articleBodies;

		public RichDocumentDeclaration()
		{ }

		public RichDocumentDeclaration(MetaMetadataCompositeField mmd) : base(mmd) { }


		public MetadataString Title
		{
			get{return title;}
			set
			{
				if (this.title != value)
				{
					this.title = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString Description
		{
			get{return description;}
			set
			{
				if (this.description != value)
				{
					this.description = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString SiteName
		{
			get{return siteName;}
			set
			{
				if (this.siteName != value)
				{
					this.siteName = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString TextKeywords
		{
			get{return textKeywords;}
			set
			{
				if (this.textKeywords != value)
				{
					this.textKeywords = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public RichDocument SeeAlso
		{
			get{return seeAlso;}
			set
			{
				if (this.seeAlso != value)
				{
					this.seeAlso = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString Query
		{
			get{return query;}
			set
			{
				if (this.query != value)
				{
					this.query = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString PageStructure
		{
			get{return pageStructure;}
			set
			{
				if (this.pageStructure != value)
				{
					this.pageStructure = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<IClipping<Metadata>> Clippings
		{
			get{return clippings;}
			set
			{
				if (this.clippings != value)
				{
					this.clippings = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public Image Favicon
		{
			get{return favicon;}
			set
			{
				if (this.favicon != value)
				{
					this.favicon = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public Image Thumbnail
		{
			get{return thumbnail;}
			set
			{
				if (this.thumbnail != value)
				{
					this.thumbnail = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<Image> MainImages
		{
			get{return mainImages;}
			set
			{
				if (this.mainImages != value)
				{
					this.mainImages = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<Video> MainVideos
		{
			get{return mainVideos;}
			set
			{
				if (this.mainVideos != value)
				{
					this.mainVideos = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<Audio> MainAudio
		{
			get{return mainAudio;}
			set
			{
				if (this.mainAudio != value)
				{
					this.mainAudio = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<Ecologylab.BigSemantics.MetadataNS.Scalar.MetadataString> ArticleBodies
		{
			get{return articleBodies;}
			set
			{
				if (this.articleBodies != value)
				{
					this.articleBodies = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}
	}
}
