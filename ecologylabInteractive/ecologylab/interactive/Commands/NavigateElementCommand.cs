using System;
using System.Windows;
using System.Windows.Input;
using ecologylab.interactive;
using ecologylab.interactive.Commands;
using ecologylab.interactive.Controls;
using ecologylab.interactive.Utils;

namespace ecologylab.interactive.Commands
{
    public class NavigateElementCommand : ICommand, ILabelledCommand
    {
        public NavigateElementCommand()
        {

        }
        public String GetLabel()
        {
            return "Navigate";
        }
        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing Navigate command");
            var parameters = (CommandParameters) parameter ;
            var hit = parameters.visualHit as UIElement;
            TextMagnifier magnifier = new TextMagnifier(hit, parameters.touchEventArgs);
        }
    }
}