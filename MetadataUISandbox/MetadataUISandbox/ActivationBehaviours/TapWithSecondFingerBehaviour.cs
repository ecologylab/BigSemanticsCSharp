using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;
using MetadataUISandbox.Utilities;

namespace MetadataUISandbox
{


    public class TapWithSecondFingerBehaviour : Behavior<UIElement>
    {
        Point? _touchHeldPos;
        //DateTime? firstUpTime;
        TouchEventArgs _touchHeld;
        bool _validSecondFingerDown = false;
        object _firstSender;

        EventHandler<TouchEventArgs> _touchDownHandler;
        EventHandler<TouchEventArgs> _touchUpHandler;
        Window _parent;
        Logger logger = new Logger();
        protected override void OnDetaching()
        {
            if (_touchDownHandler == null || _touchUpHandler == null )
                return;
            logger.Log("Detaching TapWithSecondFinger behaviour from: " + AssociatedObject);
            AssociatedObject.TouchDown -= _touchDownHandler;
            AssociatedObject.TouchUp -= _touchUpHandler;
            _touchUpHandler = null;
            _touchDownHandler = null;
        }

        void OnTouchDown(object sender, TouchEventArgs e)
        {
            TouchDevice touchDevice = e.GetTouchPoint(_parent).TouchDevice;
            Point pos = e.GetTouchPoint(_parent).Position;
            logger.Log("Touch Down");
            if (!_touchHeldPos.HasValue)
            {

                _touchHeldPos = pos;
                _touchHeld = e;
                _firstSender = sender;
                logger.Log("No Touch held, Holding: " + _touchHeld + ": " + _firstSender);
            }
            else //Could be tap
            {
                if (Utils.Distance(_touchHeldPos, pos) > 5)
                {
                    logger.Log("Second finger down");
                    double dist = Utils.Distance(_touchHeldPos, pos);
                    if (dist < 100)
                    {
                        _validSecondFingerDown = true;
                    }
                    else
                    {
                        logger.Log("Too Far");
                    }
                }
                else { logger.Log("Same finger down ?"); }

                //ClearStateVals();
            }
        }

        void OnTouchUp(object sender, TouchEventArgs e)
        {
            Point pos = e.GetTouchPoint(_parent).Position;

            if (Utils.Distance(pos, _touchHeldPos) > 20 && _validSecondFingerDown)
            {
                logger.Log("Tap from second finger !!");

                HitTestResultDelegate hitResultDelegate = (result) =>
                {
                    logger.Log("HitTest : " + result.VisualHit);
                    DependencyObject acceptableResult;
                    if ((acceptableResult = (AssociatedObject as IHitTestAcceptor).AcceptableObject(result.VisualHit)) != null)
                    {
                        logger.Log("Tap with second finger, with first finger on: " + AssociatedObject);
                        logger.Log("\tHitTest on : " + acceptableResult);

                        RightHandedControlMenu menu = new RightHandedControlMenu(_touchHeldPos.Value, _touchHeld, sender as DependencyObject, acceptableResult);

                        e.Handled = true;
                        return HitTestResultBehavior.Stop;
                    }
                    return HitTestResultBehavior.Continue;
                };
                VisualTreeHelper.HitTest(AssociatedObject, null, new HitTestResultCallback(hitResultDelegate), new PointHitTestParameters(_touchHeld.GetTouchPoint(AssociatedObject).Position));

                HitTestResult hitResult = VisualTreeHelper.HitTest(AssociatedObject, e.GetTouchPoint(AssociatedObject).Position);
            }
            else
            {
                logger.Log("First finger lifted ? ");
            }
            ClearStateVals();
        }

        protected override void OnAttached()
        {
            _parent = Application.Current.MainWindow;

            _touchDownHandler = new EventHandler<TouchEventArgs>(OnTouchDown);
            AssociatedObject.TouchDown += _touchDownHandler;

            _touchUpHandler = new EventHandler<TouchEventArgs>(OnTouchUp);
            AssociatedObject.TouchUp += _touchUpHandler;

        }
        private void ClearStateVals()
        {
            logger.Log("Clearing vals");
            //firstUpTime = null;
            _touchHeldPos = null;
            _validSecondFingerDown = false;
            //touchHeld = null;
        }


    }
}
