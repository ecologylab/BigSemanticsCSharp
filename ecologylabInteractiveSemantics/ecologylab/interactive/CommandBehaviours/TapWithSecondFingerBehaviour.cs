using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using ecologylab.interactive;
using ecologylab.interactive.Utils;

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
        static Logger logger = new Logger();

        #region Command
        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(TapWithSecondFingerBehaviour),
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
                if (Utilities.Distance(_touchHeldPos, pos) > 5)
                {
                    logger.Log("Second finger down");
                    double dist = Utilities.Distance(_touchHeldPos, pos);
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

            if (Utilities.Distance(pos, _touchHeldPos) > 20 && _validSecondFingerDown)
            {
                logger.Log("Tap from second finger !!");

                HitTestResultDelegate hitResultDelegate = (result) =>
                {
                    DependencyObject acceptableResult;
                    if ((acceptableResult = (AssociatedObject as IHitTestAcceptor).AcceptableObject(result.VisualHit)) != null)
                    {
                        logger.Log("Tap with second finger, with first finger on: " + AssociatedObject);
                        logger.Log("\tHitTest on : " + acceptableResult);
                        e.Handled = true;

                        CommandParameters commandParameters = new CommandParameters
                        {
                            touchEventArgs = _touchHeld,
                            visualContainer = sender as DependencyObject,
                            visualHit = acceptableResult
                        };
                        
                        if (command != null)
                            command.Execute(commandParameters);
                        else
                        {
                            logger.Log("No command has been bound to this behaviour.");
                        }
                        //new RightHandedControlMenu(commandParameters);


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

            this.command = (ICommand)AssociatedObject.GetValue(CommandProperty);

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
