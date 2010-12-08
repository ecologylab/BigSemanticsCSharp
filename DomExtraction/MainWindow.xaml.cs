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
using System.Collections.ObjectModel;
using ecologylab.semantics.metametadata;
using ecologylab.serialization;
using ecologylab.semantics.metadata.scalar.types;
using ecologylab.semantics.generated.library.imdb;
using ecologylab.semantics.metadata.scalar;
using System.Threading.Tasks;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using MetadataUISandbox;
using ecologylab.semantics.interactive;


namespace DomExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MMDExtractionBrowser browser = null;
        TreeViewControl control;

        string testPath = "http://www.imdb.com/title/tt1305591/";

        public MainWindow()
        {
            InitializeComponent();

            InitBrowser();
            //myCanvas.Children.Add(browser);
            //myGrid. AddChild(browser);

            control = new TreeViewControl();
            myCanvas.Children.Add(control);
            

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

            ConstructMetadataTree(result);

            //Binding b = new Binding() { Source = result, Path= new PropertyPath("Plot.Value"),  Mode = BindingMode.TwoWay };
            //BindingOperations.SetBinding(PlotTextBox, TextBox.TextProperty, b);
        }

        private void UrlTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            UrlTextbox.Text = "";
        }

        private void ConstructMetadataTree(Metadata metadata)
        {
            List<Metadata> tempList = new List<Metadata>();
            tempList.Add(metadata);
            metadataContent.Content = metadata;
            //metadataView.ItemsSource = metadata;

            /*
            Console.WriteLine("Starting construction: " + System.DateTime.Now);
            ClassDescriptor classDesc = metadata.ElementClassDescriptor;
            List<FieldDescriptor> attrs = classDesc.AttributeFieldDescriptors;
            foreach (FieldDescriptor attr in attrs)
            {
                String name = attr.FieldName;
                object value =  attr.Field.GetValue(metadata);
                if (value == null)
                    continue;
                String val = value.ToString();
                TextBlock t = new TextBlock();
                t.TextWrapping = TextWrapping.Wrap;
                t.MaxWidth = metadataView.Width;
                t.Text = name + " : " + val;
                metadataView.Items.Add(t);
            }
            Console.WriteLine("Done with construction: " + System.DateTime.Now);
            List<FieldDescriptor> elementFDs = classDesc.ElementFieldDescriptors;
            foreach (FieldDescriptor attr in elementFDs)
            {
                Console.WriteLine("attr: " + attr.FieldName);
            }*/

        }
    }
    

}
