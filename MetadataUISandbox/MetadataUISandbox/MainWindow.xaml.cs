using System;
using System.Windows;
using System.Windows.Data;
using ecologylab.semantics.metadata.builtins;
using ecologylab.serialization;
using ecologylab.semantics.metadata.scalar.types;
using System.Threading.Tasks;
using MetadataUISandbox.ActivationBehaviours;
using MetadataUISandbox.Utils;

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        
        static String workspace = @"C:\Users\damaraju.m2icode\workspace\";
        String jsPath = workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\";
        
        DoubleTapBehaviour dblTapBehaviour = new DoubleTapBehaviour();
        TapWithSecondFingerBehaviour tapSecondFingerBehaviour = new TapWithSecondFingerBehaviour();
        PressAndHoldBehaviour pressAndHoldBehaviour = new PressAndHoldBehaviour();
	    private Logger logger = new Logger();
	    private Document d;
		public MainWindow()
		{
			this.InitializeComponent();
            MetadataScalarScalarType.init();
		}


        private void OnTextSelectionChanged(object sender, RoutedEventArgs e)
        {
            BindableRichTextBox b = (sender as BindableRichTextBox);
            if (b != null)
            {
                b.Selection.Select(b.Selection.Start, b.Selection.Start);
                //b.SelectionOpacity = 0;
            }
        }

        private static Document GetMetadataFile(string filepath)
        {
            TranslationScope ts = GeneratedMetadataTranslations.Get();
            DateTime tStart = DateTime.Now;
            Document document = (Document) ts.deserialize(filepath, Format.JSON);
            Console.WriteLine("Deserialized, time : " + (DateTime.Now - tStart));
            return document;
        }

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			// Insert code required on object creation below this point.
            ((UIElement) sender).Visibility = System.Windows.Visibility.Collapsed;
            loadingAnimation.Visibility = System.Windows.Visibility.Visible;
            Console.WriteLine("Loaded TranslationScope" + DateTime.Now);
            DateTime tStart = DateTime.Now;
            
            d = (Document) await TaskEx.Run(() => GetMetadataFile(jsPath + @"tempJSON\lastMetadataCleaned.json"));
            //Document d = (Document) GetMetadataFile(jsPath + @"tempJSON\lastMetadataCleaned.json");
            TimeSpan tEnd = DateTime.Now - tStart;
            
            tStart = DateTime.Now;
            WikiView.DataContext = d;
            WikiView.Visibility = System.Windows.Visibility.Visible;
            loadingAnimation.Visibility = System.Windows.Visibility.Collapsed;
            
            Console.WriteLine("Set dataContext, time : " + (DateTime.Now - tStart));
		}

		private void SurfaceRadioButton_Checked(object sender, RoutedEventArgs e)
		{
            
            if (sender == PressAndHoldRadio && PressAndHoldRadio.IsChecked.Value)
            {
                dblTapBehaviour.Detach();
                tapSecondFingerBehaviour.Detach();
                pressAndHoldBehaviour.Attach(WikiView);
            }
            else if (sender == DoubleTapRadio && DoubleTapRadio.IsChecked.Value)
            {
                dblTapBehaviour.Attach(WikiView);
                tapSecondFingerBehaviour.Detach();
                pressAndHoldBehaviour.Detach();
            }
            else if (sender == TapWithSecondFingerRadio && TapWithSecondFingerRadio.IsChecked.Value)
            {
                tapSecondFingerBehaviour.Attach(WikiView);
                pressAndHoldBehaviour.Detach();
                dblTapBehaviour.Detach();
            }
		}
	}

    public class DoubleToIntConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double passedIn = (double)value;
            return (int)passedIn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new  NotImplementedException();
        }
    }

    

}