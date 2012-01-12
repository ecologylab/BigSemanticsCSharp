using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate.ViewModel
{
    public class MetadataFieldViewModel<M> : MetadataViewModelBase where M : MetaMetadataField
    {
       
        private readonly M          _metaMetadataField;
        private String              _fieldName           = null;

        public MetadataFieldViewModel(M metaMetadataField, Metadata metadata) : base(metadata)
        {
            this.Metadata           = metadata;
            this._metaMetadataField = metaMetadataField;

            string mmdFieldName = metaMetadataField.GetCapFieldName();
            Console.WriteLine("Binding: " + mmdFieldName);
            Binding binding = new Binding
            {
                Source = metadata,
                Path = new PropertyPath(mmdFieldName + ".Value"),
            };

            BindingOperations.SetBinding(this, FieldValueProperty, binding);
        }

        public M MetaMetadataField
        {
            get { return this._metaMetadataField; }
        }

        public String FieldName
        {
            get
            {
                _fieldName = _fieldName ?? _metaMetadataField.GetDisplayedLabel();
                return _fieldName;
            }
            set { this._fieldName = value; }
        }

        public static readonly DependencyProperty FieldValueProperty = DependencyProperty.Register("FieldValue", typeof(object), typeof(MetadataFieldViewModel<M>));

        public object FieldValue { get; set; }
    }
}
