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
using CjcAwesomiumWrapper;
using System.Collections.ObjectModel;
using ecologylab.semantics.metametadata;
using ecologylab.serialization;
using ecologylab.semantics.metadata.scalar.types;
using System.Threading.Tasks;

namespace DomExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static String uri = "http://www.imdb.com/title/tt1285016/";
        MMDExtractionBrowser extractor;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("\n\nSetting Source: " + System.DateTime.Now + "\n\n");
            
            MetadataScalarScalarType.init();
            
            //MetadataScalarScalarType.init();

            extractor = new MMDExtractionBrowser();


        }
        
        

        public async void GetMetadata()
        {
            Console.WriteLine("Getting Metadata");
            ElementState result = await extractor.ExtractMetadata(uri);
            Console.WriteLine("Got Metadata !!");
            
        }

        private void GetMetadata(object sender, RoutedEventArgs e)
        {
            TaskEx.Run(()=> GetMetadata());
        }

    }
    

}
