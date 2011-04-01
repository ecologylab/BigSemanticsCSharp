using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using MetadataUISandbox.Utils;

namespace MetadataUISandbox.Commands
{

    public class RaiseMenuCommand : ICommand, ILabelledCommand
    {

        public void Execute(object parameter)
        {
            new RightHandedControlMenu((CommandParameters)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public string GetLabel()
        {
            return "Menu2";
        }
    }

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
            elem.TouchEnter += (s, e) =>
            {
                logger.Log("Touch Enter ");
                enteredTouch = true;
            };
            elem.TouchMove += (s, e) =>
            {
                if (enteredTouch)
                {

                    return;
                }
                Point p = e.GetTouchPoint(parent).Position;

                elem.SetValue(Canvas.LeftProperty, p.X - (int)offset[0].X);
                elem.SetValue(Canvas.TopProperty, p.Y - (int)offset[0].Y);
            };

            elem.TouchDown += (s, e) =>
            {
                enteredTouch = false;
                offset[0] = e.GetTouchPoint(elem).Position;
            };


        }
    }

    public class CollapseElement : ICommand, ILabelledCommand
    {
        public String GetLabel()
        {
            return "Collapse";
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

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing CollapseWikiView command");
        }
    }

    public class ExpandElement : ICommand, ILabelledCommand
    {
        public String GetLabel()
        {
            return "Collapse";
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

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing CollapseWikiView command");
        }
    }

    public class NavigateElementCommand : ICommand, ILabelledCommand
    {
        public NavigateElementCommand()
        {

        }
        public String GetLabel()
        {
            return "Navigate";
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

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing Navigate command");
            var parameters = (CommandParameters) parameter ;
            var hit = parameters.visualHit as UIElement;
            TextMagnifier magnifier = new TextMagnifier(hit, parameters.touchEventArgs);
            

        }
    }

    public class MagnifyElementCommand : ICommand, ILabelledCommand
    {
        public MagnifyElementCommand()
        {

        }
        public String GetLabel()
        {
            return "Magnify";
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

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing Magnify command");
        }
    }

    public class BubbleCursorContainerCommand : ICommand, ILabelledCommand
    {
        public BubbleCursorContainerCommand()
        {

        }
        public String GetLabel()
        {
            return "BubbleCursorContainerCommand";
        }
        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public event EventHandler CanExecuteChanged
        {
            //Only if container is a ContentElement ?
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing BubbleCursorContainer command");
            CommandParameters cmdParams = (CommandParameters)parameter;
            DependencyObject visualHit = cmdParams.visualHit;

            Console.WriteLine("BubbleCursor on : " + visualHit );
        }
    }



}
