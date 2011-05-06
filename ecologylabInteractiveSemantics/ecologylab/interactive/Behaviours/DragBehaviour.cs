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
            private Point dragStartPosition;
            TouchDevice draggingTouchDevice;
            public bool touchDragging;
            private Canvas parent;
            public DragBehavior(Canvas dragCanvas = null, bool touchDragging = false)
            {
                parent = dragCanvas ?? VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
                this.touchDragging = touchDragging;
            }
            public DragBehavior()
            {
                
            }

            protected override void OnAttached()
            {
                parent = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
                if (touchDragging)
                {
                    AssociatedObject.TouchDown += (sender, e) =>
                    {
                        if (((List<TouchDevice>)AssociatedObject.TouchesCaptured).Count == 0)
                        {
                            elementStartPosition = AssociatedObject.TranslatePoint(new Point(), parent);
                            dragStartPosition = e.GetTouchPoint(parent).Position;

                        }
                        AssociatedObject.CaptureTouch(e.TouchDevice);
                    };

                    AssociatedObject.TouchUp += (sender, e) =>
                    {
                        AssociatedObject.ReleaseTouchCapture(e.TouchDevice);
                        var touchesCaptured = (List<TouchDevice>)AssociatedObject.TouchesCaptured;
                        if (touchesCaptured.Count == 1)
                        {
                            elementStartPosition = AssociatedObject.TranslatePoint(new Point(), parent);
                            dragStartPosition = touchesCaptured[0].GetTouchPoint(parent).Position;
                        }

                    };

                    AssociatedObject.TouchMove += (sender, e) =>
                    {
                        Vector diff = e.GetTouchPoint(parent).Position - dragStartPosition;
                        if (((List<TouchDevice>)AssociatedObject.TouchesCaptured).Count == 1)
                        {
                            AssociatedObject.SetValue(Canvas.LeftProperty, elementStartPosition.X + diff.X);
                            AssociatedObject.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                        }
                    };
                }
                else
                {

                    AssociatedObject.MouseLeftButtonDown += (sender, e) =>
                    {
                        elementStartPosition = new Point((double)AssociatedObject.GetValue(Canvas.LeftProperty),
                                                       (double)AssociatedObject.GetValue(Canvas.TopProperty));
                        dragStartPosition = e.GetPosition(parent);
                        AssociatedObject.CaptureMouse();
                    };

                    AssociatedObject.MouseLeftButtonUp += (sender, e) =>
                    {
                        AssociatedObject.ReleaseMouseCapture();
                    };

                    AssociatedObject.MouseMove += (sender, e) =>
                    {
                        Vector diff = e.GetPosition(parent) - dragStartPosition;
                        if (AssociatedObject.IsMouseCaptured)
                        {
                            AssociatedObject.SetValue(Canvas.LeftProperty, elementStartPosition.X + diff.X);
                            AssociatedObject.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                        }
                    };
                }

            }
        }
}