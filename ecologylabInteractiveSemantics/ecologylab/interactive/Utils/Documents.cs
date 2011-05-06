using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.interactive.Controls;
using Run = ecologylab.semantics.generated.library.Run;
using System.Linq;


namespace ecologylab.interactive.Utils
{
    
    public class ParsedUriToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
                if (p != null && p.Runs != null && p.Runs.Count > 0)
                    doc.Blocks.Add(HypertextParaToVisualPara(p));
            }
            else
            {
                var goodParas = from p in paras where p != null select p ;
                foreach (HypertextPara p in goodParas)
                {
                    if (p != null && p.Runs != null && p.Runs.Count > 0)
                        doc.Blocks.Add(HypertextParaToVisualPara(p));
                }
            }


            return doc;
        }

        private System.Windows.Documents.Paragraph HypertextParaToVisualPara(HypertextPara p)
        {
            var visualPara = new System.Windows.Documents.Paragraph();
            foreach (Run run in p.Runs)
            {
                var textRun = ((TextRun) run);
                if (textRun.Text == null || string.IsNullOrEmpty(textRun.Text.Value))
                    continue;
                Inline visualRun = MetadataRunToVisualRun(run);
                if(visualRun != null)
                    visualPara.Inlines.Add(visualRun);
            }
            return visualPara;
        }

        private System.Windows.Documents.Run MetadataRunToVisualRun(Run run)
        {
            var visualRun = new System.Windows.Documents.Run();
            TextRun textRun = run as TextRun;
            visualRun.Text = (string)(textRun).Text.Value;
            if (textRun.StyleInfo != null)
            {
                StyleInfo style = textRun.StyleInfo;
                if (style.Bold != null && (bool)style.Bold.Value)
                    visualRun.FontWeight = FontWeights.Bold;
                if (style.Italics != null && (bool)style.Italics.Value)
                    visualRun.FontStyle = FontStyles.Italic;
            }
            LinkRun link = (run as LinkRun);
            if (link != null)
            {
                Canvas mainCanvas = Application.Current.MainWindow.FindName("MainCanvas") as Canvas;

                visualRun.Foreground = Brushes.Blue;
                visualRun.TouchEnter += (s, e) =>
                    {
                        Console.WriteLine("Touch Enter: " + visualRun.Text);
                        visualRun.Background = Brushes.LightBlue;
                    };
                visualRun.TouchLeave += (s,e) =>
                    {
                        visualRun.Background = null;                                                               
                    };
                visualRun.TouchUp += (s, e) =>
                    {
                        Console.WriteLine("Link Selected: " + link.Location);
                        var wikiPage = new WikiPage(link.Location.Value, link.Title.Value);
                        Point position = e.GetTouchPoint(mainCanvas).Position;
                        wikiPage.SetValue(Canvas.LeftProperty, (double) position.X);
                        wikiPage.SetValue(Canvas.TopProperty, (double)position.Y);
                        mainCanvas.Children.Add(wikiPage);
                    };
                
            }

            visualRun.DataContext = run;
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
            //rtb.SelectionChanged += (s, a) => {rtb.Set}
        }
    }
}