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
using DomExtraction;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.builtins;

namespace MmTest
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MMDExtractionBrowser _browser;

    public MainWindow()
    {
      InitializeComponent();
      _browser = new MMDExtractionBrowser();
    }

    private async void BtnGetMetadata_Click(object sender, RoutedEventArgs e)
    {
      ParsedUri puri = new ParsedUri(UrlBox.Text);
      Document doc = (await _browser.ExtractMetadata(puri)) as Document;
      StringBuilder sb = new StringBuilder();
      TextWriter tw = new StringWriter(sb);
      SimplTypesScope.Serialize(doc, StringFormat.Xml, tw);
      tw.Close();
      MetadataArea.Text = sb.ToString();
    }
  }
}
