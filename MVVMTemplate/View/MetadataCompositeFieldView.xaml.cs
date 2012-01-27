using System;
using System.Windows.Controls;
using System.Windows.Input;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;
using MVVMTemplate.ViewModel;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for MetadataCompositeFieldView.xaml
    /// </summary>
    public partial class MetadataCompositeFieldView : MetadataFieldViewBase
    {
        public MetadataCompositeFieldView() : base()
        {
            InitializeComponent();
        }

        public MetadataCompositeFieldView(MetaMetadataCompositeField metaMetadataCompositeField, Metadata metadata) : base(metaMetadataCompositeField, metadata)
        {
            InitializeComponent();
        }

        protected override MetadataViewModelBase CreateViewModel(MetaMetadataField metaMetadataField, Metadata metadata)
        {
            return new MetadataCompositeFieldViewModel((MetaMetadataCompositeField) metaMetadataField, metadata);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           ((MetadataView) sender).ToggleExpand();
        }
    }
}
