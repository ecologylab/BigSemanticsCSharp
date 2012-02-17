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

        private MouseButtonEventHandler _mouseDownHandler;
        private MouseButtonEventHandler _mouseUpHandler;
        bool onlyTouch = false;
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

        public object AttachedObject{get; set;}

        public SingleTapBehaviour(bool onlyTouch = false)
        {
            this.onlyTouch = onlyTouch;
        }

        protected override void OnDetaching()
        {
            if (_mouseUpHandler != null && _mouseDownHandler != null)
            {
                AssociatedObject.MouseDown -= _mouseDownHandler;
                AssociatedObject.MouseUp -= _mouseUpHandler;
                _mouseDownHandler = null;
                _mouseUpHandler = null;
            }
            

            if (_touchDownHandler == null || _touchUpHandler == null)
                return;
            AssociatedObject.TouchUp -= _touchUpHandler;
            AssociatedObject.TouchDown -= _touchDownHandler;
            _touchUpHandler = null;
            _touchDownHandler = null;
        }

        protected override void OnAttached()
        {
            AttachedObject = AssociatedObject;
            Window parent = Application.Current.MainWindow;
            if (onlyTouch)
            {
                TouchDelegate touchDownDelegate = (sender, e) =>
                {
                    Point pos = e.GetTouchPoint(parent).Position;

                    OnDown(pos);
                };
                TouchDelegate touchUpDelegate = (sender, e) =>
                {
                    Point pos = e.GetTouchPoint(parent).Position;
                    OnUp(e, pos, sender);
                };

                _touchDownHandler = new EventHandler<TouchEventArgs>(touchDownDelegate);
                _touchUpHandler = new EventHandler<TouchEventArgs>(touchUpDelegate);
                AssociatedObject.AddHandler(UIElement.TouchDownEvent, _touchDownHandler, true);
                AssociatedObject.AddHandler(UIElement.TouchUpEvent, _touchUpHandler, true);

            }
            else
            {
                _mouseUpHandler = (s, e) =>
                {
                    Point pos = e.GetPosition(parent);
                    OnUp(e, pos, s);
                };
                _mouseDownHandler = (s, e) =>
                {
                    Point pos = e.GetPosition(parent);
                    OnDown(pos);
                };
                AssociatedObject.MouseLeftButtonDown += _mouseDownHandler;
                AssociatedObject.MouseLeftButtonUp += _mouseUpHandler;
            }
            
            
        }

        private void OnDown(Point pos)
        {
            if (!_firstDown.HasValue)
            {
                _firstDown = pos;
                _firstDownTime = DateTime.Now;
            }
        }

        private void OnUp(RoutedEventArgs e, Point pos, object sender)
        {
            if (AssociatedObject == null)
                return;

            if (Utilities.Distance(pos, _firstDown) < 30)
            {
                ////logger.Log("Within distance");
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
                            //logger.Log("SingleTap on: " + AssociatedObject);
                            //logger.Log("\tAcceptable HitTest on : " + acceptableResult);
                            e.Handled = true;
                            CommandParameters commandParameters = new CommandParameters
                            {
                                touchEventArgs = e as TouchEventArgs,
                                visualContainer = sender as DependencyObject,
                                visualHit = acceptableResult
                            };
                            if (command != null && command.CanExecute(null))
                                command.Execute(commandParameters);
                            else
                            {
                                //logger.Log("No command has been bound to this behaviour.");
                            }
                            ClearVals();

                            //new RightHandedControlMenu(commandParameters);
                            return HitTestResultBehavior.Stop;
                        }
                        return HitTestResultBehavior.Continue;
                    };
                    var mouseArgs = e as MouseEventArgs;
                    Point p = mouseArgs != null ? mouseArgs.GetPosition(AssociatedObject) : (e as TouchEventArgs).GetTouchPoint(AssociatedObject).Position;
                    VisualTreeHelper.HitTest(AssociatedObject, null, new HitTestResultCallback(hitResultDelegate), new PointHitTestParameters(p));

                    //HitTestResult hitResult = VisualTreeHelper.HitTest(AssociatedObject, e.GetTouchPoint(AssociatedObject).Position);

                    //DependencyObject hit = hitResult.VisualHit;
                    //menu.CaptureTouch(e.GetTouchPoint(parent).TouchDevice);
                }
                else
                {
                    //logger.Log("Too Slow");
                    ClearVals();
                }
            }
            else
            {
                //logger.Log("Too Far");
                ClearVals();
            }
        }

        private void ClearVals()
        {
            _firstDown = null;
            _firstDownTime = null;
        }
    }
}
