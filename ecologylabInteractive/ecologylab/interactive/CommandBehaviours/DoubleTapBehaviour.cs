using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using ecologylab.interactive;
using ecologylab.interactive.Commands;
using ecologylab.interactive.Utils;

namespace ecologylab.interactive.Behaviours
{
    
    public class DoubleTapBehaviour : Behavior<UIElement>
    {
        #region Command
        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(DoubleTapBehaviour),
                new FrameworkPropertyMetadata(
                    (ICommand)null,
                    FrameworkPropertyMetadataOptions.Inherits));

        private ICommand command;



        /// <summary>
        /// The command to be raised when the behaviour is performed by the user.
        /// 
        /// </summary>
        public ICommand Command
        {
            get { return command; }
            set { command = value; }
        }

        /// <summary>
        /// Gets the Command property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets the Command property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        #endregion

//        private EventHandler<TouchEventArgs> _touchEnterHandler;
        private EventHandler<TouchEventArgs> _touchDownHandler;
        private EventHandler<TouchEventArgs> _touchUpHandler;
        private EventHandler<TouchEventArgs> _touchMoveHandler;
//        private EventHandler<TouchEventArgs> _touchLeaveHandler;

        Window _parent;
        
        private const int DefaultDoubleTapTimeout = 1000;
        private const int DefaultMaxDistanceBetweenTaps = 30;
        private const int DefaultMaxDraggableDistancePerTap = 10;

        private readonly bool _triggerCommandOnLift;
        private readonly int _doubleTapTimeout;
        private readonly int _maxDistanceBetweenTaps;

        CommandParameters? _commandParameters;

        static Logger logger = new Logger();

        //State variables
        private bool _isReset = true;
        private Point? _firstUp;
        private Point? _firstDown;
        private Point? _secondDown;

        private DateTime? _firstUpTime;
        private bool _touchDownCaught = false;

        /// <summary>
        /// Supports two behaviours, triggering the command on lifting the finger, or on touchDown of the second tap. 
        /// The second variation is potentially useful for control menu like operations.
        /// </summary>
        /// <param name="triggerCommandOnLift">Defaults to true</param>
        /// <param name="doubleTapTimeout">Default 1 second</param>
        /// <param name="maxDistanceBetweenTaps">Taps should be within this distance (in pixels) to be accepted </param>
        public DoubleTapBehaviour(bool triggerCommandOnLift = true, int doubleTapTimeout = DefaultDoubleTapTimeout, int maxDistanceBetweenTaps = DefaultMaxDistanceBetweenTaps)
        {
            _parent = Application.Current.MainWindow;
            _triggerCommandOnLift = triggerCommandOnLift;
            _doubleTapTimeout = doubleTapTimeout;
            _maxDistanceBetweenTaps = maxDistanceBetweenTaps;
        }

        protected override void OnDetaching()
        {
            if (_touchDownHandler == null || _touchUpHandler == null)// || _touchLeaveHandler == null || _touchEnterHandler == null)
                return;
            //logger.Log("Detaching DoubleTapBehaviour from: " + AssociatedObject);
            AssociatedObject.TouchUp    -= _touchUpHandler;
            AssociatedObject.TouchDown  -= _touchDownHandler;
            AssociatedObject.TouchMove -= _touchMoveHandler;
//            AssociatedObject.TouchLeave -= _touchLeaveHandler;
//            AssociatedObject.TouchEnter -= _touchEnterHandler;

//            _touchEnterHandler  = null;
            _touchDownHandler   = null;
            _touchUpHandler     = null;
//            _touchLeaveHandler  = null;
        }

        protected override void OnAttached()
        {
//            _touchEnterHandler = AssociatedObjectOnTouchEnter;
//            AssociatedObject.AddHandler(UIElement.TouchEnterEvent, _touchEnterHandler, true);

            _touchDownHandler = AssociatedObjectOnTouchDown;
            AssociatedObject.AddHandler(UIElement.TouchDownEvent, _touchDownHandler, true);

            _touchUpHandler = AssociatedObjectOnTouchUp;
            AssociatedObject.AddHandler(UIElement.TouchUpEvent, _touchUpHandler, true);

            _touchMoveHandler = AssociatedObjectOnTouchMove;
            AssociatedObject.AddHandler(UIElement.TouchMoveEvent, _touchMoveHandler, true);


//            _touchLeaveHandler = AssociatedObjectOnTouchLeave;
//            AssociatedObject.AddHandler(UIElement.TouchLeaveEvent, _touchLeaveHandler, true);
        }

        private void AssociatedObjectOnTouchMove(object sender, TouchEventArgs e)
        {
            if (!_firstDown.HasValue) return;

            

            var p = e.GetTouchPoint(_parent).Position;
            double dist = Utilities.Distance(p, _firstDown);
            if (dist > DefaultMaxDraggableDistancePerTap)
            {
                //logger.Log("Dragged beyond tappable distance, reseting");
                ClearDblTapVals();
            }
        }

//        private void AssociatedObjectOnTouchEnter(object sender, TouchEventArgs e)
//        {
//            //logger.Log("Entered");
//        }
//
//        private void AssociatedObjectOnTouchLeave(object sender, TouchEventArgs e)
//        {
//            if (e.Device == _touchDownInputDevice)
//            {
//                //logger.Log("Touch left the AssociatedObject: " + _touchDownInputDevice);
//                ClearDblTapVals();
//            }
//            else
//            {
//                //logger.Log("Event from touchUp");
//                _touchDownInputDevice = null;    
//            }
//        }

        private void AssociatedObjectOnTouchUp(object sender, TouchEventArgs e)
        {
            if (!_touchDownCaught || !command.CanExecute(null))
            {
                //logger.Log("TouchUp without a TouchDown, resetting state.");
                ClearDblTapVals();
                return;
            }

            _touchDownCaught = false; //Reset for the next touchDown
            //logger.Log("TouchUp : firstUp " + _firstUp + ", firstDown " + _firstDown + ", firstUpTime " + _firstUpTime + ", Now:  " + DateTime.Now);
            if (!_firstUp.HasValue && _firstDown.HasValue)
            {
                _firstUpTime = DateTime.Now;
                _firstUp = e.GetTouchPoint(_parent).Position;
            }
            else
            {
                //logger.Log("Double Tap and Lift ?");
                if (_triggerCommandOnLift && command != null && _commandParameters.HasValue && command.CanExecute(null))
                {
                    command.Execute(_commandParameters.Value);
                }
                ClearDblTapVals();
            }

            //After a touch up, state must reset within the timeout
            if (HasOnlyOneTouchOver())
            {
                //TODO: Possible move could happen between touchdown and touch up
                Dispatcher.DelayInvoke(TimeSpan.FromMilliseconds(_doubleTapTimeout), () =>
                {
                    if (!_isReset)
                    {
                        //logger.Log("Timeout after touchUp, resetting state");
                        ClearDblTapVals();    
                    }
                    
                });
            }
        }

        private bool HasOnlyOneTouchOver()
        {
            return ((List<TouchDevice>)AssociatedObject.TouchesOver).Count == 1;
        }

        private void AssociatedObjectOnTouchDown(object sender, TouchEventArgs e)
        {
            _touchDownCaught = true;
            Point pos = e.GetTouchPoint(_parent).Position;
            _isReset = false;
            //logger.Log("TouchDown : firstUp " + _firstUp + ", firstDown " + _firstDown + ", firstUpTime " + _firstUpTime + ", Now:  " + DateTime.Now);
            if (!command.CanExecute(null))
            {
                ClearDblTapVals();
                //Shouldn't do anything more if the command isn't available.
                return;
            }
            if (_firstDown.HasValue)
            {
                bool resetState = true;
                if (Utilities.Distance(pos, _firstUp) < _maxDistanceBetweenTaps)
                {
                    ////logger.Log("Within distance");
                    if (DateTime.Now - _firstUpTime.Value < TimeSpan.FromMilliseconds(_doubleTapTimeout))
                    {
                        HitTestResultDelegate hitResultDelegate = (result) =>
                        {
                            var hitTestAcceptor = (AssociatedObject as IHitTestAcceptor);

                            DependencyObject acceptableResult = hitTestAcceptor != null
                                                                ? hitTestAcceptor.AcceptableObject(result.VisualHit)
                                                                : result.VisualHit;
                            if (acceptableResult != null)
                            {
                                //logger.Log("DoubleTap on: " + AssociatedObject);
                                //logger.Log("\tAcceptable HitTest on : " + acceptableResult);
                                e.Handled = true;

                                _commandParameters = new CommandParameters
                                                        {
                                                            touchEventArgs = e,
                                                            visualContainer = sender as DependencyObject,
                                                            visualHit = acceptableResult
                                                        };
                                if (command != null && command.CanExecute(null))
                                {
                                    if (!_triggerCommandOnLift)
                                        command.Execute(_commandParameters.Value);
                                    else
                                    {
                                        //Should we reset the state after a while?
                                        //logger.Log("Waiting for lift-off");
                                        resetState = false;
                                    }
                                }
                                else
                                {
                                    //logger.Log("No command has been bound to this behaviour.");
                                    resetState = true;
                                }
                                return HitTestResultBehavior.Stop;
                            }
                            return HitTestResultBehavior.Continue;
                        };
                        VisualTreeHelper.HitTest(AssociatedObject, null, new HitTestResultCallback(hitResultDelegate),
                                                 new PointHitTestParameters(e.GetTouchPoint(AssociatedObject).Position));

                        //HitTestResult hitResult = VisualTreeHelper.HitTest(AssociatedObject, e.GetTouchPoint(AssociatedObject).Position);

                        //DependencyObject hit = hitResult.VisualHit;
                        //menu.CaptureTouch(e.GetTouchPoint(_parent).TouchDevice);
                    }
                    else
                    {
                        //logger.Log("Too Slow");
                    }
                }
                else
                {
                    //logger.Log("Too Far");
                }
                if (resetState) //Only avoid resetting the state if we're expecting and waiting for a liftoff
                    ClearDblTapVals();

            }
            else
            {
                _firstDown = pos;
            }

            //If the finger is on here for more than a second, disregard possibility of a double tap.
            Dispatcher.DelayInvoke(TimeSpan.FromMilliseconds(_doubleTapTimeout), () =>
            {
                //If !triggerCommandOnLift, we expect the finger to stay on the UIElement
                if (_isReset || !_touchDownCaught || !_triggerCommandOnLift) return;

                //logger.Log("Finger is on UIElement for too long, resetting state");
                ClearDblTapVals();
            });
        }

        private void ClearDblTapVals()
        {
            //logger.Log("Clearing vals");
            _touchDownCaught        = false;
            _firstUpTime            = null;
            _firstUp                = null;
            _firstDown              = null;
            _commandParameters      = null;
            _isReset                = true;
        }


    }
}
