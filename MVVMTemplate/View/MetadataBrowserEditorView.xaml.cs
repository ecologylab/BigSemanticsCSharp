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
using Simpl.Serialization;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for MetadataBrowserEditorView.xaml
    /// </summary>
    public partial class MetadataBrowserEditorView : UserControl
    {
        public MetadataBrowserEditorView(Metadata metadata)
        {
            InitializeComponent();

            MetadataBrowserEditorViewModel viewModel            = new MetadataBrowserEditorViewModel(metadata);
            this.DataContext                                    = viewModel;

//            MetaMetadataOneLevelNestingEnumerator enumerator    = viewModel.DisplayedFields;
//            this.FieldsView.BuildFields(enumerator);
        }

    }
}
