using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ecologylab.interactive.Behaviours
{
    public class TouchResizer : Behavior<UIElement>
    {
        private double elementStartWidth;
        private Point elementStartPosition;
        private Dictionary<TouchDevice, Point> deviceStartPoints = new Dictionary<TouchDevice, Point>();

        private Canvas parent = null;
        private UIElement canvasChild = null;

        public TouchResizer(Canvas parentCanvas = null, UIElement canvasChild = null)
        {
            parent = parentCanvas;
            this.canvasChild = canvasChild;
        }


        protected override void OnAttached()
        {
            parent = parent?? VisualTreeHelper.GetParent(AssociatedObject) as Canvas;

            AssociatedObject.TouchDown += (sender, e) =>
            {
                int count = ((List<TouchDevice>)AssociatedObject.TouchesCaptured).Count;
                if (count == 0 || count == 1)
                {
                    elementStartPosition = new Point((double)canvasChild.GetValue(Canvas.LeftProperty),
                                                     (double)canvasChild.GetValue(Canvas.TopProperty));
                    elementStartWidth = (double)AssociatedObject.GetValue(FrameworkElement.WidthProperty);
                    TouchDevice d = e.TouchDevice;
                    Point p = e.GetTouchPoint(parent).Position;
                    if(deviceStartPoints.ContainsKey(d))
                        deviceStartPoints.Remove(d);
                    deviceStartPoints.Add(d, p);
                }
                AssociatedObject.CaptureTouch(e.TouchDevice);
            };

            AssociatedObject.TouchUp += (sender, e) =>
            {
                AssociatedObject.ReleaseTouchCapture(e.TouchDevice);
                var touchesCaptured = (List<TouchDevice>)AssociatedObject.TouchesCaptured;

                deviceStartPoints.Remove(e.TouchDevice);
            };

            AssociatedObject.TouchMove += (sender, e) =>
            {
                List<TouchDevice> touchDevices = ((List<TouchDevice>)AssociatedObject.TouchesCaptured);

                if (touchDevices.Count == 2)
                {
                    TouchDevice thisTouchDevice = e.TouchDevice;
                    touchDevices.Remove(thisTouchDevice);
                    TouchDevice otherTouchDevice = touchDevices[0];
                    Point prevPosition;
                    Point otherPrevPosition;
                    if (deviceStartPoints.TryGetValue(thisTouchDevice, out prevPosition) && deviceStartPoints.TryGetValue(otherTouchDevice, out otherPrevPosition))
                    {
                        Point position = e.GetTouchPoint(parent).Position;
                        Vector diff = position - prevPosition;
                        if (Math.Abs(diff.X) > 3)
                        {
                            double newWidth = 0;
                            if (otherPrevPosition.X < prevPosition.X)
                            {
                                newWidth = elementStartWidth + diff.X;
                                
                            }
                            else
                            {
                                newWidth = elementStartWidth - diff.X;

                                if (canvasChild != null)
                                {
                                    canvasChild.SetValue(Canvas.LeftProperty, elementStartPosition.X + diff.X);
                                    elementStartPosition = new Point((double)canvasChild.GetValue(Canvas.LeftProperty),
                                                                        (double)canvasChild.GetValue(Canvas.TopProperty));
                                }
                                //canvasElement.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                            }

                            AssociatedObject.SetValue(FrameworkElement.WidthProperty, newWidth);
                            double value = (double)AssociatedObject.GetValue(FrameworkElement.WidthProperty);
                            elementStartWidth = value;
                            //Console.WriteLine("New Width : " + value, " Diff: " + diff);
                            //AssociatedObject.SetValue(Canvas.TopProperty, elementStartPosition.Y + diff.Y);
                            deviceStartPoints.Remove(thisTouchDevice);
                            deviceStartPoints.Add(thisTouchDevice, position);
                        }
            
                        
                        
                    }
                }
            };
            
        }
    }
}
