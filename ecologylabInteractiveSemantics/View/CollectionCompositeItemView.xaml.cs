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
    /// Interaction logic for CollectionCompositeItemView.xaml
    /// </summary>
    public partial class CollectionCompositeItemView : MetadataViewBase
    {
       
        public CollectionCompositeItemView(MetaMetadataCompositeField metaMetadataCompositeField, Metadata metadata, int nestedLevel)
        {
            // is the right meta-metadata stored in metadata, or do we need to pass it in here?
            this.DataContext = new MetadataViewModel(metadata);
            
            InitializeComponent();
            this.FieldLabel.ExpandButton.CommandTarget = this.FieldValue;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           ((MetadataView) sender).ToggleExpand();
        }

        public override Grid LayoutGrid
        {
            get { return LayoutRoot; }
        }

    }
}
