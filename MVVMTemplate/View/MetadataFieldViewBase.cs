using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;
using MVVMTemplate.ViewModel;

namespace MVVMTemplate.View
{
    public abstract class MetadataFieldViewBase : MetadataViewBase
    {

        public MetadataFieldViewBase() : base()
        {
            this.Loaded += new RoutedEventHandler(MetadataFieldView_Loaded);
        }

         public MetadataFieldViewBase(MetaMetadataField metaMetadataField, Metadata metadata, int nestedLevel)
        {
           this.DataContext = CreateViewModel(metaMetadataField, metadata, nestedLevel);
        }

        private void MetadataFieldView_Loaded(object sender, RoutedEventArgs e)
        {
            //use inherited parent's DataContext (ViewModel) to get Metadata and MetaMetadata
            if (this.DataContext != null && this.MetaMetadataFieldName != null)
            {
                Metadata metadata = ((MetadataViewModelBase) this.DataContext).Metadata;
                MetaMetadataField metaMetadataField =
                    (MetaMetadataScalarField) metadata.MetaMetadata.LookupChild(this.MetaMetadataFieldName);

                this.DataContext = CreateViewModel(metaMetadataField, metadata);
            }
        }
        
        public String MetaMetadataFieldName { get; set; }

        protected abstract MetadataViewModelBase CreateViewModel(MetaMetadataField metaMetadataField, Metadata metadata, int nestedLevel = 0);
    }
}
