using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    public class MetadataCollectionCompositeItemViewModel : MetadataNestedFieldViewModel<MetaMetadataCompositeField>
    {
        public MetadataCollectionCompositeItemViewModel(MetaMetadataCompositeField metaMetadataField, Metadata metadata, int nestedLevel) : base(metaMetadataField, metadata, nestedLevel)
        {
        }

        #region Overrides of MetadataViewModelBase

        public override bool MultipleVisibleFields
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override Binding CreateBinding(Metadata metadata, string mmdFieldName)
        {
            return new Binding
                {
                    Source = metadata,
                    Path = new PropertyPath("."),
                };
        }
    }
}
