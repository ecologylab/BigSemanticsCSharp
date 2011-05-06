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
            private Canvas parent = null;
            private UIElement canvasChild = null;
            public DragBehavior(Canvas dragCanvas = null, UIElement canvasChild = null,  bool touchDragging = false)
            {
                parent = dragCanvas;
                this.canvasChild = canvasChild;
                this.touchDragging = touchDragging;
            }


            protected override void OnAttached()
            {
                parent = parent?? VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
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
                    UIElement canvasElement = canvasChild ?? AssociatedObject;
                    AssociatedObject.MouseLeftButtonDown += (sender, e) =>
                    {
                        elementStartPosition = new Point((double)canvasElement.GetValue(Canvas.LeftProperty),
                                                       (double)canvasElement.GetValue(Canvas.TopProperty));
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
                            canvasElement.SetValue(Canvas.LeftProperty, elementStartPosition.X + diff.X);
                            canvasElement.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                        }
                    };
                }

            }
        }
}