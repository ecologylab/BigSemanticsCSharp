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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ecologylab.semantics.generated.library;

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for WikiPage.xaml
	/// </summary>
	public partial class WikiPage : UserControl
	{
		public WikiPage()
		{
			this.InitializeComponent();
		}
		
	}
	

    public class HypertextToFlowDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FlowDocument doc = new FlowDocument();

            List<HypertextPara> paras = value as List<HypertextPara>;

            if (paras == null)
            {
                HypertextPara p = value as HypertextPara;
                if(p != null && p.Runs != null && p.Runs.Count > 0)
                    doc.Blocks.Add(HypertextParaToVisualPara(p));
            }
            else
            {
                foreach (HypertextPara p in paras)
                {
                    if (p != null && p.Runs != null && p.Runs.Count > 0)
                        doc.Blocks.Add(HypertextParaToVisualPara(p));
                }
            }

            
            return doc;
        }

        private System.Windows.Documents.Paragraph HypertextParaToVisualPara(HypertextPara p)
        {
            System.Windows.Documents.Paragraph visualPara = new System.Windows.Documents.Paragraph();

            foreach (ecologylab.semantics.generated.library.Run run in p.Runs)
            {
                Inline visualRun = MetadataRunToVisualRun(run);
                visualPara.Inlines.Add(visualRun);
            }
            return visualPara;
        }

        private System.Windows.Documents.Run MetadataRunToVisualRun(ecologylab.semantics.generated.library.Run run)
        {
            System.Windows.Documents.Run visualRun = new System.Windows.Documents.Run();
            TextRun textRun = run as TextRun;
            visualRun.Text = (string) (textRun).Text.Value;
            if(textRun.StyleInfo != null)
            {
                StyleInfo style = textRun.StyleInfo;
                if (style.Bold != null && (bool)style.Bold.Value)
                    visualRun.FontWeight = FontWeights.Bold;
                if (style.Italics != null && (bool) style.Italics.Value)
                    visualRun.FontStyle = FontStyles.Italic;
            }
            LinkRun link = (run as LinkRun);
            if( link != null)
            {
                visualRun.Foreground = Brushes.Blue;
            }
            return visualRun;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

	public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDocumentChanged)));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RichTextBox rtb = (RichTextBox)obj;
            rtb.Document = (FlowDocument)args.NewValue;
        }
    }
}