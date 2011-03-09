using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using ecologylab.semantics.generated.library;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Diagnostics;

/// Forgive the mess. This will get organized when there are well defined places they need to be in.

namespace MetadataUISandbox.Utilities
{


    public class Utils
    {
        public static double Distance(Point? a, Point? b)
        {
            if (!a.HasValue || !b.HasValue)
                return Double.NaN;
            double deltaX = (a.Value.X - b.Value.X);
            double deltaY = (a.Value.Y - b.Value.Y);
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }

    public class Logger
    {
        String _logPrefix;
        short clsLength;
        public Logger(short clsLength = 40)
        {
            this.clsLength = clsLength;
            ResetPrefix();
        }

        public void ResetPrefix()
        {
            Type declType = new StackFrame(2, false).GetMethod().DeclaringType;
            String callingClassName = (declType.DeclaringType == null ? declType.Name : declType.DeclaringType.Name);
            _logPrefix = "[     " + callingClassName.Substring(0, callingClassName.Length < clsLength ? callingClassName.Length : clsLength).PadRight(clsLength, ' ') +
                                "]: ";
        }

        public void Log(String val)
        {
            Console.WriteLine( _logPrefix + val);
        }

    }
    
    public interface ILabelledCommand
    {
        String GetLabel();
    }

    /// <summary>
    /// Allows behaviours to request the AssociatedObject for an AcceptableObject while iterating through the visual hit test results.
    /// 
    /// Ideally, this would have been only a boolean. 
    /// However, visual hit tests on RichTextObjects are funny, we might have to return an element out of the hitTestZone as the accepted object. 
    /// 
    /// </summary>
    public interface IHitTestAcceptor
    {
        DependencyObject AcceptableObject(DependencyObject obj);
    }

    delegate HitTestResultBehavior HitTestResultDelegate(HitTestResult result);

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
            //rtb.SelectionChanged += (s, a) => {rtb.Set}
        }
    }
}
