using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ecologylab.semantics.metadata;

namespace MVVMTemplate.ViewModel
{
    public class MetadataViewModelBase : DependencyObject
    {
        public MetadataViewModelBase(Metadata metadata)
        {
            this.Metadata = metadata;
        }

        public Metadata Metadata { get; set; }

    }
}
