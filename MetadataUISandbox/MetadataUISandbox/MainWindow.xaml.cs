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
using System.IO;
using MetadataUISandbox.ActivationBehaviours;
using MetadataUISandbox.Utilities;

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
        DoubleTapBehaviour dblTapBehaviour = new DoubleTapBehaviour();
        TapWithSecondFingerBehaviour tapSecondFingerBehaviour = new TapWithSecondFingerBehaviour();
        PressAndHoldBehaviour pressAndHoldBehaviour = new PressAndHoldBehaviour();
		public MainWindow()
		{
			this.InitializeComponent();
            MetadataScalarScalarType.init();
            ts = GeneratedMetadataTranslations.Get();


		}


        private void OnTextSelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            BindableRichTextBox b = (sender as BindableRichTextBox);
            if (b != null)
            {
                b.SelectionOpacity = 0;
            }
        }

		private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// Insert code required on object creation below this point.
            (sender as UIElement).Visibility = Visibility.Collapsed;
            loadingAnimation.Visibility = Visibility.Visible;
            Console.WriteLine("Loaded TranslationScope" + System.DateTime.Now);
            DateTime tStart = System.DateTime.Now;
            Document d = (Document) await TaskEx.Run(() => ts.deserialize(jsPath + @"tempJSON\lastMetadataCleaned.json", Format.JSON));
            //Document d = (Document)ts.deserialize(jsPath + @"tempJSON\lastMetadataCleaned.json", Format.JSON);
            TimeSpan tEnd = System.DateTime.Now - tStart;
            Console.WriteLine("Deserialized, time : " + tEnd);
            tStart = System.DateTime.Now;
            WikiView.DataContext = d;
            WikiView.Visibility = System.Windows.Visibility.Visible;
            loadingAnimation.Visibility = System.Windows.Visibility.Collapsed;
            
            Console.WriteLine("Set dataContext, time : " + (System.DateTime.Now - tStart));
		}

		private void SurfaceRadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
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