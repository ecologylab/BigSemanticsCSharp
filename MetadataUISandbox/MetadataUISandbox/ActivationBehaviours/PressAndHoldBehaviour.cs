﻿using System;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MetadataUISandbox.Utils;
using Microsoft.Surface.Presentation.Controls;

namespace MetadataUISandbox
{

    public class PressAndHoldBehaviour : Behavior<UIElement>
    {
        delegate void TickCompletedDelegate(object sender, EventArgs args);
        bool deactivated = false;
        EventHandler<TouchEventArgs> touchDownHandler;
        EventHandler<TouchEventArgs> touchUpHandler;
        EventHandler<TouchEventArgs> touchMoveHandler;

        Logger logger = new Logger();
        protected override void OnDetaching()
        {
            if (touchDownHandler == null || touchUpHandler == null || touchMoveHandler == null)
                return;
            logger.Log("Detaching PressAndHoldBehaviour from: " + AssociatedObject);
            AssociatedObject.TouchDown -= touchDownHandler;
            AssociatedObject.TouchUp -= touchUpHandler;
            AssociatedObject.TouchMove -= touchMoveHandler;
            touchUpHandler = null;
            touchDownHandler = null;
            touchMoveHandler = null;
        }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            
            Point? firstTouch= null;

            #region defining and connecting touchHandlers
            
            TouchDelegate touchDownDelegate = (sender, e) =>
            {
                deactivated = false;
                TouchDevice touchDevice = e.GetTouchPoint(parent).TouchDevice;
                Point pos = e.GetTouchPoint(parent).Position;
                logger.Log("Touch Down");
                firstTouch = e.GetTouchPoint(parent).Position;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds((parent.FindName("timeoutSlider") as SurfaceSlider).Value);
                timer.Start();
                timer.Tick += (s, ev) =>
                {
                    if (deactivated)
                    {
                        logger.Log("Deactivated");
                        timer.Stop();
                        return;
                    }
                    

                    HitTestResultDelegate hitResultDelegate = (result) =>
                    {
                        DependencyObject acceptableResult;
                        if ((acceptableResult = (AssociatedObject as IHitTestAcceptor).AcceptableObject(result.VisualHit)) != null)
                        {
                            logger.Log("Press and held on: " + AssociatedObject + "\n\tHitTest on : " + acceptableResult);
                            e.Handled = true;

                            CommandParameters commandParameters = new CommandParameters
                                                                      {
                                                                          touchEventArgs = e,
                                                                          visualContainer = sender as DependencyObject,
                                                                          visualHit = acceptableResult
                                                                      };

                            new RightHandedControlMenu(commandParameters);
                            
                            return HitTestResultBehavior.Stop;
                        }
                        return HitTestResultBehavior.Continue;
                    };

                    VisualTreeHelper.HitTest(AssociatedObject, null, new HitTestResultCallback(hitResultDelegate), new PointHitTestParameters(e.GetTouchPoint(AssociatedObject).Position));

                    HitTestResult hitResult = VisualTreeHelper.HitTest(AssociatedObject, e.GetTouchPoint(AssociatedObject).Position);

                    timer.Stop();  
                };
            };

            touchDownHandler = new EventHandler<TouchEventArgs>(touchDownDelegate);
            AssociatedObject.TouchDown += touchDownHandler;

            TouchDelegate touchMoveDelegate = (sender, e) =>
            {
                if (Utilities.Distance(e.GetTouchPoint(parent).Position, firstTouch.Value) > 20)
                {
                    deactivated = true;
                }
            };

            touchMoveHandler = new EventHandler<TouchEventArgs>(touchMoveDelegate);
            AssociatedObject.TouchMove += touchMoveHandler;


            TouchDelegate touchUpDelegate = (sender, e) =>
            {
                Point pos = e.GetTouchPoint(parent).Position;
                deactivated = true;
                ClearStateVals();
            };

            touchUpHandler = new EventHandler<TouchEventArgs>(touchUpDelegate);
            AssociatedObject.TouchUp += touchUpHandler;
            #endregion
        }
        private void ClearStateVals()
        {
            logger.Log("Clearing vals");
            //firstUpTime = null;
            //touchHeldPos = null;
            //touchHeld = null;
        }



    }
}
