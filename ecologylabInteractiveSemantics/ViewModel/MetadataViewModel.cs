using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    public class MetadataViewModel : MetadataViewModelBase, INotifyPropertyChanged
    {
        public MetadataViewModel(Metadata metadata) : base(metadata)
        {
        }

        public override bool MultipleVisibleFields
        {
            get { return this.Metadata.NumberOfVisibleFields() > 1; }
        }
    }
}
