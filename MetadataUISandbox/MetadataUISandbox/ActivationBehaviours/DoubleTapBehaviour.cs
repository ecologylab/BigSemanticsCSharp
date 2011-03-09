using System;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using MetadataUISandbox.Utilities;

namespace MetadataUISandbox.ActivationBehaviours
{
    
    public class DoubleTapBehaviour : Behavior<UIElement>
    {
        Point? _firstUp;
        Point? _firstDown;
        DateTime? _firstUpTime;

        EventHandler<TouchEventArgs> _touchDownHandler;
        EventHandler<TouchEventArgs> _touchUpHandler;
        Logger logger = new Logger();

        protected override void OnDetaching()
        {
            if (_touchDownHandler == null || _touchUpHandler == null)
                return;
            logger.Log("Detaching DoubleTapBehaviour from: " + AssociatedObject);
            AssociatedObject.TouchUp -= _touchUpHandler;
            AssociatedObject.TouchDown -= _touchDownHandler;
            _touchUpHandler = null;
            _touchDownHandler = null;
        }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            TouchDelegate touchDownDelegate = (sender, e) =>
            {
                Point pos = e.GetTouchPoint(parent).Position;

                logger.Log("TouchDown : firstUp " + _firstUp + ", firstDown " + _firstDown + ", firstUpTime " + _firstUpTime + ", Now:  " + DateTime.Now);

                if (!_firstDown.HasValue)
                {
                    _firstDown = pos;
                }
                else //Could be tap
                {
                    if (Utils.Distance(pos, _firstUp) < 30)
                    {
                        //logger.Log("Within distance");
                        if (DateTime.Now - _firstUpTime.Value < TimeSpan.FromMilliseconds(1000))
                        {

                            HitTestResultDelegate hitResultDelegate = (result) => 
                            {
                                DependencyObject acceptableResult;
                                if ( (acceptableResult =  (AssociatedObject as IHitTestAcceptor).AcceptableObject(result.VisualHit)) != null)
                                {
                                    logger.Log("DoubleTap on: " + AssociatedObject + "\n\tHitTest on : " + acceptableResult);
                                    e.Handled = true;
                                    RightHandedControlMenu menu = new RightHandedControlMenu(pos, e, sender as DependencyObject, acceptableResult);
                                    return HitTestResultBehavior.Stop;
                                }
                                return HitTestResultBehavior.Continue;
                            };
                            VisualTreeHelper.HitTest(AssociatedObject, null , new HitTestResultCallback(hitResultDelegate), new PointHitTestParameters( e.GetTouchPoint(AssociatedObject).Position));

                            //HitTestResult hitResult = VisualTreeHelper.HitTest(AssociatedObject, e.GetTouchPoint(AssociatedObject).Position);

                            //DependencyObject hit = hitResult.VisualHit;
                            //menu.CaptureTouch(e.GetTouchPoint(parent).TouchDevice);
                        }
                        else
                        {
                            logger.Log("Too Slow");
                        }
                    }
                    else
                    {
                        logger.Log("Too Far");
                    }

                    ClearDblTapVals();
                }
            };

            _touchDownHandler = new EventHandler<TouchEventArgs>(touchDownDelegate);

            AssociatedObject.TouchDown += _touchDownHandler;


            TouchDelegate touchUpDelegate = (sender, e) =>
            {
                logger.Log("TouchUp : firstUp " + _firstUp + ", firstDown " + _firstDown + ", firstUpTime " + _firstUpTime + ", Now:  " + DateTime.Now);
                if (!_firstUp.HasValue && _firstDown.HasValue)
                {
                    _firstUpTime = DateTime.Now;
                    _firstUp = e.GetTouchPoint(parent).Position;
                }
                else
                {
                    logger.Log("Double Tap and Lift ?");
                    ClearDblTapVals();
                }
            };

            _touchUpHandler = new EventHandler<TouchEventArgs>(touchUpDelegate);

            AssociatedObject.TouchUp += _touchUpHandler;
        }

        private void ClearDblTapVals()
        {
            logger.Log("Clearing vals");
            _firstUpTime = null;
            _firstUp = null;
            _firstDown = null;
        }
    }
}
