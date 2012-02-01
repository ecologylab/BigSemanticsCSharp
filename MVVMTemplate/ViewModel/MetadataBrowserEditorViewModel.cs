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
    public class MetadataBrowserEditorViewModel : INotifyPropertyChanged
    {
        private Metadata _metadata;

        public MetaMetadataOneLevelNestingEnumerator DisplayedFields
        {
            get { return Metadata.MetaMetadataIterator(); }
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

        public MetadataBrowserEditorViewModel(Metadata metadata)
        {
            Metadata = metadata;

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
    }
}
