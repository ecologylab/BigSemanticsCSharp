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
    public class MetadataScalarFieldViewModel : MetadataFieldViewModel<MetaMetadataScalarField>
    {
        public MetadataScalarFieldViewModel(MetaMetadataScalarField metaMetadataField, Metadata metadata ) : base(metaMetadataField, metadata)
        {

        }

        protected override Binding CreateBinding(Metadata metadata, string mmdFieldName)
        {
            return new Binding
                {
                    Source = metadata,
                    Path = new PropertyPath(mmdFieldName + ".Value"),
                };
        }

        #region Overrides of MetadataViewModelBase

        public override bool MultipleVisibleFields
        {
            get { return false; }
        }

        #endregion
    }
}
