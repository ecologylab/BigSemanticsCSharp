using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ecologylab.interactive;
using ecologylab.interactive.Utils;
using ecologylabInteractiveSemantics.ecologylab.interactive.Behaviours;

namespace ecologylab.semantics.interactive.Commands
{
    public class CopyElementCommand : ICommand, ILabelledCommand
    {
        Logger logger = new Logger();

        public String GetLabel()
        {
            return "Copy";
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Adds one flowdocument to another.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void AddDocument(FlowDocument from, FlowDocument to)
        {
            TextRange range = new TextRange(from.ContentStart, from.ContentEnd);

            MemoryStream stream = new MemoryStream();

            System.Windows.Markup.XamlWriter.Save(range, stream);
            range.Save(stream, DataFormats.XamlPackage);

            TextRange range2 = new TextRange(to.ContentEnd, to.ContentEnd);

            range2.Load(stream, DataFormats.XamlPackage);
        }

        public void Execute(object parameters)
        {

            CommandParameters cmdParams = (CommandParameters)parameters;
            logger.Log("Executing Copy command with parameter: " + cmdParams.visualHit);
            Image img = cmdParams.visualHit as Image;
            BindableRichTextBox box = cmdParams.visualHit as BindableRichTextBox;
            UIElement elem = null;
            Point[] offset = {new Point()};
            if (img != null)
            {
                //Download original again ? 
                
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)img.Source.Width, (int)img.Source.Height, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(img);
                Image imgCopy = new Image {Source = bitmap};
                elem = imgCopy;
                offset[0] = new Point(img.ActualWidth / 2, img.ActualHeight / 2);
            }
            else if (box != null)
            {
                FlowDocument doc = new FlowDocument();
                RichTextBox boxCopy = new RichTextBox {Width = box.ActualWidth, IsReadOnly = true, Document = doc};
                
                AddDocument(box.Document, doc);
                

                offset[0] = new Point(50, 50);
                elem = boxCopy;
                logger.Log("VisualHit is : " + cmdParams.visualHit);
            }

            Window parent = Application.Current.MainWindow;
            Canvas mainCanvas = (Canvas)parent.FindName("MainCanvas");
            //Position must pass through in the parameter
            TouchPoint touchPoint = cmdParams.touchEventArgs.GetTouchPoint(parent);
            Point loc = touchPoint.Position;

            logger.Log("Adding Element: " + elem);
            elem.SetValue(Canvas.LeftProperty, loc.X - (int)offset[0].X);
            elem.SetValue(Canvas.TopProperty, loc.Y - (int)offset[0].Y);
            mainCanvas.Children.Add(elem);
            elem.CaptureTouch(touchPoint.TouchDevice);
            bool enteredTouch = false;
            DragBehavior draggable = new DragBehavior();
            draggable.Attach(elem);
        }
    }
}