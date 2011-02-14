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
using ecologylab.serialization;
using ecologylab.semantics.metadata.scalar.types;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        
        static String workspace = @"C:\Users\damaraju.m2icode\workspace\";
        String jsPath = workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\";
        TranslationScope ts;
		public MainWindow()
		{
			this.InitializeComponent();

            MetadataScalarScalarType.init();
            ts = GeneratedMetadataTranslations.Get();
		}

		private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// Insert code required on object creation below this point.


            Console.WriteLine("Loaded TranslationScope" + System.DateTime.Now);
            DateTime tStart = System.DateTime.Now;
            Document d = (Document) await TaskEx.Run(() => ts.deserialize(jsPath + @"tempJSON\lastMetadataCleaned.json", Format.JSON));
            //Document d = (Document)ts.deserialize(jsPath + @"tempJSON\lastMetadataCleaned.json", Format.JSON);
            TimeSpan tEnd = System.DateTime.Now - tStart;
            Console.WriteLine("Deserialized, time : " + tEnd);
            tStart = System.DateTime.Now;
            WikiView.DataContext = d;
            WikiView.Visibility = System.Windows.Visibility.Visible;
            Console.WriteLine("Set dataContext, time : " + (System.DateTime.Now - tStart));
		}
	}
}