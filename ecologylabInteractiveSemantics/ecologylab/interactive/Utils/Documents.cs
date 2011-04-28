﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using ecologylab.semantics.generated.library;
using Run = ecologylab.semantics.generated.library.Run;

namespace ecologylab.interactive.Utils
{
    
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

            foreach (Run run in p.Runs)
            {
                Inline visualRun = MetadataRunToVisualRun(run);
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