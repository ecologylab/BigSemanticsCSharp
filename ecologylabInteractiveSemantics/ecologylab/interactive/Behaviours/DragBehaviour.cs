using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ecologylabInteractiveSemantics.ecologylab.interactive.Behaviours
{
    public class DragBehavior : Behavior<UIElement>
    {
        private Point elementStartPosition;
        private Point mouseStartPosition;

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.MouseLeftButtonDown += (sender, e) =>
            {
                elementStartPosition = new Point((double)AssociatedObject.GetValue(Canvas.LeftProperty),
                                               (double)AssociatedObject.GetValue(Canvas.TopProperty));
                mouseStartPosition = e.GetPosition(parent);
                AssociatedObject.CaptureMouse();
            };

            AssociatedObject.MouseLeftButtonUp += (sender, e) =>
            {
                AssociatedObject.ReleaseMouseCapture();
            };

            AssociatedObject.MouseMove += (sender, e) =>
            {
                Vector diff = e.GetPosition(parent) - mouseStartPosition;
                if (AssociatedObject.IsMouseCaptured)
                {
                    AssociatedObject.SetValue(Canvas.LeftProperty, elementStartPosition.X + diff.X);
                    AssociatedObject.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                }
            };
        }
    }
}
