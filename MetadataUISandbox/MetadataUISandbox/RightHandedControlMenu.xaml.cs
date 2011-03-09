using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MetadataUISandbox.Utilities;

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for RightHandedControlMenu.xaml
	/// </summary>
    /// 

    public struct CommandParameters
    {
        public DependencyObject visualHit;

        public TouchEventArgs touchEventArgs;

        public DependencyObject visualContainer;
    }

	public partial class RightHandedControlMenu : UserControl
	{
	    readonly Logger _logger;
	    readonly List<UIElement> _zones;
	    readonly List<String> zoneCommandNames;

	    DependencyObject visualHit;
        DependencyObject visualContainer;

        DispatcherTimer popUpTimer;
        bool menuPopped = false;

        DispatcherTimer menuFadeAwayTimer;

		public RightHandedControlMenu()
		{
		    _logger = new Logger();
		    _zones = new List<UIElement>();
		    zoneCommandNames = new List<String>();
		    this.InitializeComponent();
            _logger = new Logger();
		}

        public RightHandedControlMenu(Point pos, TouchEventArgs e, DependencyObject visualContainer, DependencyObject visualElement)
        {
            _logger = new Logger();
            _zones = new List<UIElement>();
            zoneCommandNames = new List<String>();

            //Set your commands from the object (upwards ?)
            this.InitializeComponent();
            Window parent = Application.Current.MainWindow;

            UIElement capturer = (UIElement) e.TouchDevice.Captured;

            if (capturer != null)
            {
                _logger.Log("Event args touch device was capture by: " + capturer);
                capturer.ReleaseAllTouchCaptures();
            }

            e.TouchDevice.Capture(this);
            e.Handled = true;
            this.visualHit = visualElement;
            this.visualContainer = visualContainer;
            
            //Find border for visualElement and Animating Highlight
            DispatcherTimer captureTimer = new DispatcherTimer();
            captureTimer.Interval = TimeSpan.FromMilliseconds(100);
            captureTimer.Tick += (s, a) =>
            {
                _logger.Log("Capture Timer completed");

                foreach (TouchDevice device in this.TouchesOver)
                {
                    _logger.Log("Touches over : " + device.Id);
                    device.Capture(this);
                }
                captureTimer.Stop();
            };

            object obj = visualElement;

            #region Initializing Zone and Command stuff

            #region Populate zones
            
            _zones.Add(NorthVisual);
            _zones.Add(NorthWestVisual);
            _zones.Add(WestVisual);
            _zones.Add(SouthWestVisual);
            _zones.Add(SouthVisual);
            _zones.Add(SouthEastVisual);
            _zones.Add(EastVisual);
            _zones.Add(NorthEastVisual);

            #endregion

            #region Populate zoneCommandProperties
            zoneCommandNames.Add("NorthZoneCommand");
            zoneCommandNames.Add("NorthWestZoneCommand");
            zoneCommandNames.Add("WestZoneCommand");
            zoneCommandNames.Add("SouthWestZoneCommand");
            zoneCommandNames.Add("SouthZoneCommand");
            zoneCommandNames.Add("SouthEastZoneCommand");
            zoneCommandNames.Add("EastZoneCommand");
            zoneCommandNames.Add("NorthEastZoneCommand");
            #endregion

            
            _logger.Log("Picking up commands");
            do
            {
                //logger.Log("\tIterating over : " + obj );
                DependencyObject el = (obj as DependencyObject);

                LookForAndAttachCommand(el);

                obj = VisualTreeHelper.GetParent(el);
            } while (obj != visualContainer);

            LookForAndAttachCommand(visualContainer as DependencyObject);
           
            
            /*NorthZoneCommand = (visualContainer as UIElement).GetValue(NorthZoneCommandProperty) as ICommand;
            NorthWestZoneCommand = (visualContainer as UIElement).GetValue(NorthWestZoneCommandProperty) as ICommand;
            WestZoneCommand = (visualContainer as UIElement).GetValue(WestZoneCommandProperty) as ICommand;
            SouthWestZoneCommand = (visualContainer as UIElement).GetValue(SouthWestZoneCommandProperty) as ICommand;
            SouthZoneCommand = (visualContainer as UIElement).GetValue(SouthZoneCommandProperty) as ICommand;*/
            

            NorthVisual_Label.Text = NorthZoneCommand != null ? (NorthZoneCommand as ILabelledCommand).GetLabel() : "";
            NorthWestVisual_Label.Text = NorthWestZoneCommand != null ? (NorthWestZoneCommand as ILabelledCommand).GetLabel() : "";
            WestVisual_Label.Text = WestZoneCommand != null ? (WestZoneCommand as ILabelledCommand).GetLabel() : "";
            SouthWestVisual_Label.Text = SouthWestZoneCommand != null ? (SouthWestZoneCommand as ILabelledCommand).GetLabel() : "";
            SouthVisual_Label.Text = SouthZoneCommand != null ? (SouthZoneCommand as ILabelledCommand).GetLabel() : "";
            #endregion

            Canvas mainCanvas = (Canvas)parent.FindName("MainCanvas");

            #region fading menu on touchup

            menuFadeAwayTimer = new DispatcherTimer();
            menuFadeAwayTimer.Interval = TimeSpan.FromSeconds(2);

            DoubleAnimation menuFadeResetAnim = new DoubleAnimation(1, TimeSpan.FromSeconds(0));
            bool isFadingAway = false;
            menuFadeAwayTimer.Tick += (s, a) =>
            {
                //Fade away slowly
                DoubleAnimation menuFadeAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(3));
                menuFadeAnim.Completed += (se, args) =>
                {
                    if (isFadingAway)
                    {
                        _logger.Log("Menu faded");
                        mainCanvas.Children.Remove(this);
                    }
                };
                _logger.Log("Is fading away");
                isFadingAway = true;
                this.BeginAnimation(UIElement.OpacityProperty, menuFadeAnim);
                (s as DispatcherTimer).Stop(); //convention, can use menuFadeAwayTimer too.
            };

            this.TouchUp += (s, a) =>
            {
                _logger.Log("Starting menuFadeAwayTimer");
                menuFadeAwayTimer.Start();
            };

            this.TouchDown += (s, a) =>
            {
                _logger.Log("Stopping menuFadeAwayTimer");
                this.BeginAnimation(OpacityProperty, menuFadeResetAnim);
                isFadingAway = false;
                menuFadeAwayTimer.Stop();

            };
            #endregion

            #region Attach to canvas
            
            //Set position on MainCanvas
            this.SetValue(Canvas.LeftProperty, pos.X - 125);
            this.SetValue(Canvas.TopProperty, pos.Y - 125);
            mainCanvas.Children.Add(this);

            VisualStateManager.GoToState(this, "Collapsed", false);
            popUpTimer = new DispatcherTimer();
            popUpTimer.Interval = TimeSpan.FromSeconds(.5);
            popUpTimer.Tick += (s, a) =>
            {
                _logger.Log("Expanding Menu");
                VisualStateManager.GoToState(this, "Expanded", true);
                menuPopped = true;
                popUpTimer.Stop();
            };
            popUpTimer.Start();
            #endregion

        }
        
        private void LookForAndAttachCommand(DependencyObject element)
        {
            //Iterate through CommandProperties to see if we should add them this menu.
            foreach (string zoneCommandName in zoneCommandNames)
            {
                Type type = this.GetType();
                DependencyProperty zoneDpProp = (DependencyProperty) type.GetField(zoneCommandName + "Property").GetValue(this);
                ICommand elementDeclaredCommand;

                if((elementDeclaredCommand  = (ICommand) element.GetValue(zoneDpProp)) == null)
                    continue;
                
                ICommand menuCommand = this.GetValue(zoneDpProp) as ICommand;
                if (menuCommand == null)
                {
                    PropertyInfo propertyInfo = type.GetProperty(zoneCommandName);
                    propertyInfo.SetValue(this, elementDeclaredCommand, null);
                    _logger.Log("\tAdding Command: " + elementDeclaredCommand + " on zoneProp: " + zoneCommandName + " declared on : " + element);
                }
                else
                {
                    _logger.Log("Not overwriting command: " + menuCommand + " defined on: " + element + " for zone: " + zoneDpProp);
                }
            }
        }

	    private void RightHandedControlMenu_TouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Point pos = e.GetTouchPoint(this).Position;
            double x = pos.X;
            double y = pos.Y;

            HitTestResult res = VisualTreeHelper.HitTest(this, pos);

            UIElement obj = (UIElement) res.VisualHit;
            
            if (obj != InnerCircle)
            {
                CommandParameters parameters = new CommandParameters();
                parameters.visualHit = visualHit;
                parameters.touchEventArgs = e;
                parameters.visualContainer = visualContainer;

                #region call command
                if (obj == North)
                {
                    _logger.Log("North Zone Command: " + (NorthZoneCommand == null ? "null" : NorthZoneCommand.ToString()));

                    if (NorthZoneCommand != null)
                    {
                        NorthZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == NorthWest)
                {
                    _logger.Log("NorthWest Zone Command: " + (NorthWestZoneCommand == null ? "null" : NorthWestZoneCommand.ToString()));

                    if (NorthWestZoneCommand != null)
                    {
                        NorthWestZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == West)
                {
                    _logger.Log("West Zone Command: " + (WestZoneCommand == null ? "null" : WestZoneCommand.ToString()));

                    if (WestZoneCommand != null)
                    {
                        WestZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == SouthWest)
                {
                    _logger.Log("SouthWest Zone Command: " + (SouthWestZoneCommand == null ? "null" : SouthWestZoneCommand.ToString()));

                    if (SouthWestZoneCommand != null)
                    {
                        SouthWestZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == South)
                {
                    _logger.Log("South Zone Command: " + (SouthZoneCommand == null ? "null" : SouthZoneCommand.ToString()));

                    if (SouthZoneCommand != null)
                    {
                        SouthZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == SouthEast)
                {
                    _logger.Log("SouthEast Zone Command: " + (SouthEastZoneCommand == null ? "null" : SouthEastZoneCommand.ToString()));

                    if (SouthEastZoneCommand != null)
                    {
                        SouthEastZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == East)
                {
                    _logger.Log("East Zone Command: " + (EastZoneCommand == null ? "null" : EastZoneCommand.ToString()));

                    if (EastZoneCommand != null)
                    {
                        EastZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                else if (obj == NorthEast)
                {
                    _logger.Log("NorthEast Zone Command: " + (NorthEastZoneCommand == null ? "null" : NorthEastZoneCommand.ToString()));

                    if (NorthEastZoneCommand != null)
                    {
                        NorthEastZoneCommand.Execute(parameters);
                        //e.Handled = true;
                    }
                }
                #endregion
                
                //Visual and Selection zones are different, for visual reasons. 
                popUpTimer.Stop();
                UIElement visualZone = (UIElement)this.FindName((String)obj.GetValue(FrameworkElement.NameProperty) + "Visual");
                CollapseAllButDelayZone(visualZone);
                
                //Remove listener
                this.TouchMove -= RightHandedControlMenu_TouchMove;
            }
        }



        private delegate void AnimationCompletedDelegate(object sender, EventArgs args);

        private void CollapseAllButDelayZone(UIElement ignoreZone)
        {
            VisualStateManager.GoToState(this, "Expanded", false);
            foreach (UIElement zone in _zones)
            {
                UIElement labelForZone = GetUITextForElement(zone);

                if (zone != ignoreZone)
                {
                    if (!menuPopped)
                    {
                        //Set opacity to zero instantly. Using DoubleAnimations seems to be the best way to do so.
                        DoubleAnimation anim = new DoubleAnimation((double)zone.GetValue(UIElement.OpacityProperty), 0, TimeSpan.FromMilliseconds(0));
                        zone.BeginAnimation(UIElement.OpacityProperty, anim);
                        if(labelForZone != null)
                            labelForZone.BeginAnimation(UIElement.OpacityProperty, anim);
                    }
                    FadeOutZone(zone, new UIElement[] {GetUITextForElement(zone)});
                }
                else
                {
                    //First display if zone is in collapsed stage.
                    if (!menuPopped)
                    {
                        DoubleAnimation anim = new DoubleAnimation((double)zone.GetValue(UIElement.OpacityProperty), 1, TimeSpan.FromMilliseconds(0));
                        zone.BeginAnimation(UIElement.OpacityProperty, anim);
                        if (labelForZone != null)
                            labelForZone.BeginAnimation(UIElement.OpacityProperty, anim);
                    }
                    
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(.75);
                    UIElement zoneDelegateHolder = zone;
                   

                    timer.Tick += (sender, args) =>
                    {
                        
                        AnimationCompletedDelegate animCompleted = new AnimationCompletedDelegate((s, a) =>
                        {
                            Window win = Application.Current.MainWindow;
                            Canvas canvas = (Canvas)win.FindName("MainCanvas");
                            _logger.Log("Removing menu from MainCanvas");
                            
                            canvas.Children.Remove(this);
                        });
                        FadeOutZone(zoneDelegateHolder, new UIElement[] { labelForZone, MenuCenter }, animationCompleted: animCompleted, delay: 500);
                        (sender as DispatcherTimer).Stop();
                    };
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Fade out a zone of the Marking menu. Attach the OnCompleted delegate if any
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        private void FadeOutZone(UIElement zone, UIElement[] otherElements = null , AnimationCompletedDelegate animationCompleted = null, double delay=500)
        {

            
            if(otherElements != null)
                foreach(UIElement otherElement in otherElements)
                    if (otherElement != null)
                    {
                        DoubleAnimation anim = new DoubleAnimation((double)otherElement.GetValue(UIElement.OpacityProperty), 0, TimeSpan.FromMilliseconds(delay));
                        otherElement.BeginAnimation(UIElement.OpacityProperty, anim);
                    }

            DoubleAnimation mainAnim = new DoubleAnimation((double)zone.GetValue(UIElement.OpacityProperty), 0, TimeSpan.FromMilliseconds(delay));

            if (animationCompleted != null)
                mainAnim.Completed += new EventHandler(animationCompleted);
            zone.BeginAnimation(UIElement.OpacityProperty, mainAnim);
            
        }

        private UIElement GetUITextForElement(UIElement element)
        {
            string textName = element.GetValue(FrameworkElement.NameProperty) + "_Label";
            UIElement textZone = (UIElement)FindName(textName);
            return textZone;
        }

        #region Commands
        
        #region NorthZoneCommand
        ICommand northZoneCommand;

        public ICommand NorthZoneCommand
        {
            get { return northZoneCommand; }
            set { northZoneCommand = value; }
        }

        /// <summary>
        /// NorthZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty NorthZoneCommandProperty =
            DependencyProperty.RegisterAttached("NorthZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the NorthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetNorthZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(NorthZoneCommandProperty);
        }

        /// <summary>
        /// Sets the NorthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetNorthZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(NorthZoneCommandProperty, value);
        }

        #endregion

        #region NorthWestZoneCommand
        ICommand northWestZoneCommand;

        public ICommand NorthWestZoneCommand
        {
            get { return northWestZoneCommand; }
            set { northWestZoneCommand = value; }
        }

        /// <summary>
        /// NorthZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty NorthWestZoneCommandProperty =
            DependencyProperty.RegisterAttached("NorthWestZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the NorthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetNorthWestZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(NorthWestZoneCommandProperty);
        }

        /// <summary>
        /// Sets the NorthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetNorthWestZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(NorthWestZoneCommandProperty, value);
        }

        #endregion

        #region WestZoneCommand
        ICommand westZoneCommand;

        public ICommand WestZoneCommand
        {
            get { return westZoneCommand; }
            set { westZoneCommand = value; }
        }

        /// <summary>
        /// WestZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty WestZoneCommandProperty =
            DependencyProperty.RegisterAttached("WestZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the WestZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetWestZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(WestZoneCommandProperty);
        }

        /// <summary>
        /// Sets the WestZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetWestZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(WestZoneCommandProperty, value);
        }

        #endregion

        #region SouthWestZoneCommand
        ICommand southWestZoneCommand;

        public ICommand SouthWestZoneCommand
        {
            get { return southWestZoneCommand; }
            set { southWestZoneCommand = value; }
        }

        /// <summary>
        /// SouthWestZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SouthWestZoneCommandProperty =
            DependencyProperty.RegisterAttached("SouthWestZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the SouthWestZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetSouthWestZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(SouthWestZoneCommandProperty);
        }

        /// <summary>
        /// Sets the SouthWestZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSouthWestZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(SouthWestZoneCommandProperty, value);
        }

        #endregion

        #region SouthZoneCommand
        ICommand southZoneCommand;

        public ICommand SouthZoneCommand
        {
            get { return southZoneCommand; }
            set { southZoneCommand = value; }
        }

        /// <summary>
        /// SouthZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SouthZoneCommandProperty =
            DependencyProperty.RegisterAttached("SouthZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the SouthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetSouthZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(SouthZoneCommandProperty);
        }

        /// <summary>
        /// Sets the SouthZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSouthZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(SouthZoneCommandProperty, value);
        }

        #endregion

        #region SouthEastZoneCommand
        ICommand southEastZoneCommand;

        public ICommand SouthEastZoneCommand
        {
            get { return southEastZoneCommand; }
            set { southEastZoneCommand = value; }
        }

        /// <summary>
        /// SouthEastZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SouthEastZoneCommandProperty =
            DependencyProperty.RegisterAttached("SouthEastZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the SouthEastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetSouthEastZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(SouthEastZoneCommandProperty);
        }

        /// <summary>
        /// Sets the SouthEastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSouthEastZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(SouthEastZoneCommandProperty, value);
        }

        #endregion

        #region EastZoneCommand
        ICommand eastZoneCommand;

        public ICommand EastZoneCommand
        {
            get { return eastZoneCommand; }
            set { eastZoneCommand = value; }
        }

        /// <summary>
        /// EastZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty EastZoneCommandProperty =
            DependencyProperty.RegisterAttached("EastZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the EastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetEastZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(EastZoneCommandProperty);
        }

        /// <summary>
        /// Sets the EastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetEastZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(EastZoneCommandProperty, value);
        }

        #endregion

        #region NorthEastZoneCommand
        ICommand northEastZoneCommand;

        public ICommand NorthEastZoneCommand
        {
            get { return northEastZoneCommand; }
            set { northEastZoneCommand = value; }
        }

        /// <summary>
        /// SouthEastZoneCommand Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty NorthEastZoneCommandProperty =
            DependencyProperty.RegisterAttached("NorthEastZoneCommand", typeof(ICommand), typeof(RightHandedControlMenu),
                new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Gets the NorthEastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetNorthEastZoneCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(NorthEastZoneCommandProperty);
        }

        /// <summary>
        /// Sets the NorthEastZoneCommand property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetNorthEastZoneCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(NorthEastZoneCommandProperty, value);
        }

        #endregion
        #endregion
    }
}