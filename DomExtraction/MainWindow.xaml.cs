using System;
using System.Windows;
using System.Threading.Tasks;
using ecologylab.semantics.metadata.builtins;


namespace DomExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MMDExtractionBrowser browser = null;
        
        string testPath = "http://en.wikipedia.org/wiki/Hyderabad,_India";

        public MainWindow()
        {
            InitializeComponent();

            InitBrowser();
            //myCanvas.Children.Add(browser);
            //myGrid. AddChild(browser);

            //control = new TreeViewControl();
            //myCanvas.Children.Add(control);
        }


        
        private async void InitBrowser()
        {
            GetMetadataButton.IsEnabled = false;

            browser = new MMDExtractionBrowser();
            await TaskEx.Run(() => browser.InitRepo()); 

            GetMetadataButton.IsEnabled = true;

        }



        private void GetMetadataButton_Click(object sender, RoutedEventArgs e)
        {
            DoStuff();
        }

        private async void DoStuff()
        {
            Console.WriteLine("Getting Metadata" + System.DateTime.Now);
            Document result = (Document)await browser.ExtractMetadata(uri: testPath);

            //Execution resumes when result is obtained.
            //In this case, the UI thread resumes normal processing of interaction,
            //returning to the following code, that would normally have been in a callback function.
            Console.WriteLine("Got Metadata for page :: " + result.Title + " at " + System.DateTime.Now);

            //WikiViewer.DataContext = result;

            //Binding b = new Binding() { Source = result, Path= new PropertyPath("Plot.Value"),  Mode = BindingMode.TwoWay };
            //BindingOperations.SetBinding(PlotTextBox, TextBox.TextProperty, b);
        }

        private void UrlTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            UrlTextbox.Text = "";
        }

    }
    

}
