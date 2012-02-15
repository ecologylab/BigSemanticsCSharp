using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using ecologylab.semantics.metadata;

namespace MVVMTemplate.ViewModel
{
    public abstract class MetadataViewModelBase : DependencyObject
    {
        private Metadata _metadata;

        protected MetadataViewModelBase(Metadata metadata)
        {
            this.Metadata = metadata;
        }

        public Metadata Metadata
        {
            get { return _metadata; }
            set
            {
                if (_metadata != value)
                {
                    _metadata = value;
                    OnPropertyChanged("Metadata");
                }  
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public abstract bool MultipleVisibleFields { get; }

    }
}
