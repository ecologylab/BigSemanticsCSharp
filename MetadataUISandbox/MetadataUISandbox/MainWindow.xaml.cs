using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.net;

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        Document d;
		public MainWindow()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
            d = new Document();
            d.Title.Value = "This is the title";
            d.Location.Value = new ParsedUri("http://localhost/");
            d.Description.Value = "Lorem ipsum ... some decently long description of the document. The length of this field could extend to more characters too, support clipping and expanding";
		}
	}
}