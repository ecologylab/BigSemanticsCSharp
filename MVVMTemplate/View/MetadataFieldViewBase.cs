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
    public abstract class MetadataFieldViewBase : UserControl
    {
        
        public MetadataFieldViewBase()
        {
            this.Loaded += new RoutedEventHandler(MetadataFieldView_Loaded);
        }

         public MetadataFieldViewBase(MetaMetadataField metaMetadataField, Metadata metadata)
        {
           this.DataContext = CreateViewModel(metaMetadataField, metadata);
        }

        private void MetadataFieldView_Loaded(object sender, RoutedEventArgs e)
        {
            //use inherited parent's DataContext (ViewModel) to get Metadata and MetaMetadata
            if (this.DataContext == null && this.MetaMetadataFieldName != null)
            {
                Metadata metadata = ((MetadataViewModelBase) this.DataContext).Metadata;
                MetaMetadataField metaMetadataField =
                    (MetaMetadataScalarField) metadata.MetaMetadata.LookupChild(this.MetaMetadataFieldName);

                this.DataContext = CreateViewModel(metaMetadataField, metadata);
            }
        }
        
        public String MetaMetadataFieldName { get; set; }

        protected abstract MetadataViewModelBase CreateViewModel(MetaMetadataField metaMetadataField, Metadata metadata);
    }
}
