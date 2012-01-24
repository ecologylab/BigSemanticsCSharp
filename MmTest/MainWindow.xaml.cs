using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.collecting;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace MmTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SemanticsSessionScope _semanticsSessionScope;

        public MainWindow()
        {
            InitializeComponent();
            _semanticsSessionScope = new SemanticsSessionScope(
                RepositoryMetadataTranslationScope.Get(),
                MetaMetadataRepositoryInit.DEFAULT_REPOSITORY_LOCATION
                );

        }

        private async void BtnGetMetadata_Click(object sender, RoutedEventArgs e)
        {
            MetadataArea.Text = null;
            ParsedUri puri = new ParsedUri(UrlBox.Text);
            var parsedDoc = await _semanticsSessionScope.GetDocument(puri);
            
            MetadataArea.Text = SimplTypesScope.Serialize(parsedDoc, StringFormat.Xml);
            
        }



    }
}
