using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for ExpandCollapseButton.xaml
    /// </summary>
    public partial class ExpandCollapseButton : UserControl, ICommandSource
    {
        private IInputElement   _commandTarget;
        private object          _commandParameter;

        private EventHandler    _canExecuteChangedHandler;

        public ExpandCollapseButton()
        {
            InitializeComponent();
        }

        #region Implementation of ICommandSource

        // Make Command a dependency property so it can use databinding.
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(ExpandCollapseButton),
                new PropertyMetadata((ICommand)null,
                new PropertyChangedCallback(CommandChanged)));



        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExpandCollapseButton button = (ExpandCollapseButton) d;
            button.HookUpCommand((ICommand)e.OldValue,(ICommand)e.NewValue);
        }

        public ICommand Command
        {
            get 
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set 
            {
                SetValue(CommandProperty, value);
            }
        }

        // Add a new command to the Command Property.
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            // If oldCommand is not null, then we need to remove the handlers.
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
            AddCommand(oldCommand, newCommand);
        }

        // Remove an old command from the Command Property.
        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        // Add the command.
        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            _canExecuteChangedHandler = handler;
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += _canExecuteChangedHandler;
            }
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {

            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                // If a RoutedCommand.
                if (command != null)
                {
                    if (command.CanExecute(CommandParameter, CommandTarget))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
                // If a not RoutedCommand.
                else
                {
                    if (Command.CanExecute(CommandParameter))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
            }
        }

        public object CommandParameter
        {
            get { return _commandParameter; }
            set { _commandParameter = value;  }
        }

        public IInputElement CommandTarget
        {
            get { return _commandTarget; }
            set { _commandTarget = value; }
        }

        #endregion

        public bool IsExpanded { get; set; }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
                "IsExpanded",
                typeof(bool),
                typeof(ExpandCollapseButton),
                new PropertyMetadata(false, new PropertyChangedCallback(IsExpandedChanged)));



        private static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExpandCollapseButton button = (ExpandCollapseButton) d;
            button.SetValue(IsExpandedProperty, e.NewValue);
        }

        public bool IsPressed { get; set; }

         // Make Command a dependency property so it can use databinding.
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register(
                "IsPressed",
                typeof(bool),
                typeof(ExpandCollapseButton),
                new PropertyMetadata(false, new PropertyChangedCallback(IsPressedChanged)));



        private static void IsPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExpandCollapseButton button = (ExpandCollapseButton) d;
            button.SetValue(IsPressedProperty, e.NewValue);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            this.IsPressed = true;

            this.Ellipse.Fill = PressedColor;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            this.IsPressed = false;

            this.Ellipse.Fill = HoverColor;
            ToggleState();

            if (this.Command != null)
            {
                RoutedCommand command = Command as RoutedCommand;

                if (command != null)
                {
                    command.Execute(CommandParameter, CommandTarget);
                }
                else
                {
                    ((ICommand)Command).Execute(CommandParameter);
                }
            }
        }

        private void ToggleState()
        {
            IsExpanded = !IsExpanded;
            Symbol.Text = (IsExpanded) ? "-" : "+";
        }

        public Brush BackgroundColor { get; set; }
        public Brush HoverColor { get; set; }
        public Brush PressedColor { get; set; }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            this.Ellipse.Fill = HoverColor; //new SolidColorBrush(Color.FromRgb(136, 136, 136));
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            this.Ellipse.Fill = BackgroundColor; //new SolidColorBrush(Color.FromRgb(85, 85, 85));
        }


    }
}
