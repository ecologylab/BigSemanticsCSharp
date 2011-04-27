using System;
using System.Windows;
using System.Windows.Input;
using ecologylab.interactive;
using ecologylab.interactive.Utils;

namespace ecologylab.semantics.interactive.Commands
{
    public class BubbleCursorContainerCommand : ICommand, ILabelledCommand
    {
        public BubbleCursorContainerCommand()
        {

        }
        public String GetLabel()
        {
            return "BubbleCursorContainerCommand";
        }
        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public event EventHandler CanExecuteChanged
        {
            //Only if container is a ContentElement ?
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing BubbleCursorContainer command");
            CommandParameters cmdParams = (CommandParameters)parameter;
            DependencyObject visualHit = cmdParams.visualHit;

            Console.WriteLine("BubbleCursor on : " + visualHit );
        }
    }



}
