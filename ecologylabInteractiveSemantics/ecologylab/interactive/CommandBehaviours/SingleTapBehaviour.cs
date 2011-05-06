using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using ecologylab.interactive.Utils;
using ecologylabInteractiveSemantics.ecologylab.interactive.Utils;

namespace ecologylab.interactive.CommandBehaviours
{
    public class SingleTapBehaviour : Behavior<UIElement>
    {
        private DateTime? _firstDownTime;
        private Point? _firstDown;
        static Logger logger = new Logger();
        EventHandler<TouchEventArgs> _touchDownHandler;
        EventHandler<TouchEventArgs> _touchUpHandler;


        #region Command
        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(SingleTapBehaviour),
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
            if (_touchDownHandler == null || _touchUpHandler == null)
                return;
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

                if (!_firstDown.HasValue)
                {
                    _firstDown = pos;
                    _firstDownTime = DateTime.Now;
                }
                else //Could be tap
                {
                    

                   // ClearDblTapVals();
                }
            };

            _touchDownHandler = new EventHandler<TouchEventArgs>(touchDownDelegate);

            AssociatedObject.AddHandler(UIElement.TouchDownEvent, _touchDownHandler, true);

            //AssociatedObject.TouchDown += _touchDownHandler;


            TouchDelegate touchUpDelegate = (sender, e) =>
            {
                Point pos = e.GetTouchPoint(parent).Position;
                if (Utilities.Distance(pos, _firstDown) < 30)
                {
                    //logger.Log("Within distance");
                    if (DateTime.Now - _firstDownTime.Value < TimeSpan.FromMilliseconds(300))
                    {
                        HitTestResultDelegate hitResultDelegate = (result) =>
                        {
                            var hitTestAcceptor = (AssociatedObject as IHitTestAcceptor);

                            DependencyObject acceptableResult = hitTestAcceptor != null
                                                                ? hitTestAcceptor.AcceptableObject(result.VisualHit)
                                                                : result.VisualHit;
                            if (acceptableResult != null)
                            {
                                logger.Log("DoubleTap on: " + AssociatedObject);
                                logger.Log("\tAcceptable HitTest on : " + acceptableResult);
                                e.Handled = true;
                                CommandParameters commandParameters = new CommandParameters
                                {
                                    touchEventArgs = e,
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
                        VisualTreeHelper.HitTest(AssociatedObject, null, new HitTestResultCallback(hitResultDelegate), new PointHitTestParameters(e.GetTouchPoint(AssociatedObject).Position));

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
            };

            _touchUpHandler = new EventHandler<TouchEventArgs>(touchUpDelegate);

            AssociatedObject.AddHandler(UIElement.TouchUpEvent, _touchUpHandler, true);
            //AssociatedObject.TouchUp += _touchUpHandler;
        }
    }
}
