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
            this.FieldLabel.ExpandButton.CommandTarget = this.FieldValue;
        }

        public MetadataCompositeFieldView(MetaMetadataCompositeField metaMetadataCompositeField, Metadata metadata, int nestedLevel) : base(metaMetadataCompositeField, metadata, nestedLevel)
        {
            InitializeComponent();
            this.FieldLabel.ExpandButton.CommandTarget = this.FieldValue;
        }

        protected override MetadataViewModelBase CreateViewModel(MetaMetadataField metaMetadataField, Metadata metadata, int nestedLevel = 0)
        {
            return new MetadataCompositeFieldViewModel((MetaMetadataCompositeField) metaMetadataField, metadata, nestedLevel);
        }

        public override Grid LayoutGrid
        {
            get { return LayoutRoot; }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           ((MetadataView) sender).ToggleExpand();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MetadataView metadataView = ((MetadataView) sender);
            e.CanExecute = true;
        }
    }
}
