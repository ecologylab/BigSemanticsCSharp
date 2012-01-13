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
using Simpl.Serialization.Context;
using ecologylab.semantics.generated.library.products;
using ecologylab.semantics.metadata;
using MVVMTemplate.View;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.collecting;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace MVVMTemplate
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

        private void LoadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	
            ParsedUri puri = new ParsedUri(UrlBox.Text);
            if (!puri.IsFile)
            {
                _semanticsSessionScope.GetDocument(puri, (parsedDoc) =>
                                                             {

                                                                 MetadataBrowserEditorView
                                                                     docTemplatedMetadataBrowserEditorView =
                                                                         new MetadataBrowserEditorView(parsedDoc);


                                                                 canvas.Children.Add(
                                                                     docTemplatedMetadataBrowserEditorView);
                                                             });
            }
            else
            {
                var metadata = (Metadata) _semanticsSessionScope.MetadataTranslationScope.Deserialize(
                    new FileInfo(puri.LocalPath),
                    new TranslationContext(),
                    new MetadataDeserializationHookStrategy(_semanticsSessionScope),
                    Format.Xml);
                MetadataBrowserEditorView docTemplatedMetadataBrowserEditorView = 
                     new MetadataBrowserEditorView(metadata);

                canvas.Children.Add(docTemplatedMetadataBrowserEditorView);
            }
        }
    }
}
