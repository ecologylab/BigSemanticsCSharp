using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;
using MVVMTemplate.ViewModel;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for MetadataCollectionFieldView.xaml
    /// </summary>
    public partial class MetadataCollectionFieldView : MetadataFieldViewBase
    {
        public MetadataCollectionFieldView() : base()
        {
            InitializeComponent();
        }

        public MetadataCollectionFieldView(MetaMetadataCollectionField metaMetadataCompositeField, Metadata metadata) : base(metaMetadataCompositeField, metadata)
        {
            InitializeComponent();
        }

        protected override MetadataViewModelBase CreateViewModel(MetaMetadataField metaMetadataField, Metadata metadata)
        {
            return new MetadataCollectionFieldViewModel((MetaMetadataCollectionField) metaMetadataField, metadata);
        }
    }
}
