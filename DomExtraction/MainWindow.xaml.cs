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
using ecologylab.semantics.generated.library.imdb;


namespace DomExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static String uri = "http://www.imdb.com/title/tt1285016/";
        //MMDExtractionBrowser browser;
        public MainWindow()
        {
            InitializeComponent();
            //browser = new MMDExtractionBrowser();
            //myCanvas.Children.Add(browser);


            //myGrid. AddChild(browser);
        }


        private async void GetMetadata(object sender, RoutedEventArgs e)
        {

            
            Console.WriteLine("Getting Metadata");
            ImdbTitle result = (ImdbTitle) await  browser.ExtractMetadata(UrlTextbox.Text);
            Console.WriteLine("Got Metadata :: " + result.Plot);
            Plot.Text = result.Plot.Value;
        }

    }
    

}
