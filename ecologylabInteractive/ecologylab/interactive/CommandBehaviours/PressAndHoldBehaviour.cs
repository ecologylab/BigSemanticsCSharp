using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;
using ecologylab.interactive;
using ecologylab.interactive.Commands;
using ecologylab.interactive.Utils;
using ecologylabInteractiveSemantics.ecologylab.interactive.Utils;
using Microsoft.Surface.Presentation.Controls;

namespace ecologylab.interactive.Behaviours
{

    public class PressAndHoldBehaviour : Behavior<UIElement>
    {
        delegate void TickCompletedDelegate(object sender, EventArgs args);
        bool deactivated = false;
        EventHandler<TouchEventArgs> touchDownHandler;
        EventHandler<TouchEventArgs> touchUpHandler;
        EventHandler<TouchEventArgs> touchMoveHandler;

        #region Command
        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(PressAndHoldBehaviour),
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
