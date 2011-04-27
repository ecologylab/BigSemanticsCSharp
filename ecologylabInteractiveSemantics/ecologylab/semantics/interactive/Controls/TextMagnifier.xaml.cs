using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ecologylabInteractiveSemantics.ecologylab.interactive.Utils;

namespace ecologylab.semantics.interactive.Controls
{
	/// <summary>
	/// Interaction logic for TextMagnifier.xaml
	/// </summary>
	public partial class TextMagnifier : UserControl
	{
	    private UIElement _target;


	    private TouchDelegate moveDelegate;

	    public TextMagnifier(UIElement hit, TouchEventArgs touchEventArgs)
		{
			this.InitializeComponent();

            this._target = hit;
            var window = Application.Current.MainWindow;

            Canvas mainCanvas = (Canvas) window.FindName("MainCanvas");
            mainCanvas.Children.Add(this);
            magnifyRegion.Visual = _target;
            int yOffset = 10; //Distance the magnifier is away from touchPoint

            var canvasPos = touchEventArgs.GetTouchPoint(window).Position;
            Canvas.SetLeft(this, canvasPos.X - this.Width / 2.0);
            Canvas.SetTop(this, canvasPos.Y - this.Height - yOffset);

            moveDelegate = (s, e) =>
            {
                VisualBrush b = magnifyRegion;
                var pos = e.GetTouchPoint(_target).Position;
                var viewBox = b.Viewbox;
                var xOffset = viewBox.Width / 2.0;

                viewBox.X = pos.X - xOffset;
                viewBox.Y = pos.Y - viewBox.Height / 2.0;
                b.Viewbox = viewBox;
                pos = e.GetTouchPoint(window).Position;
                Canvas.SetLeft(this, pos.X - this.Width / 2.0);
                Canvas.SetTop(this, pos.Y - this.Height - yOffset);
            };

            var moveHandler = new EventHandler<TouchEventArgs>(moveDelegate);

            _target.TouchMove += moveHandler;

            _target.TouchUp += (s, e) =>
            {
                Console.WriteLine("Removing Magnifier");
                _target.TouchMove -= moveHandler;
                mainCanvas.Children.Remove(this);
            };
        }
    }
}