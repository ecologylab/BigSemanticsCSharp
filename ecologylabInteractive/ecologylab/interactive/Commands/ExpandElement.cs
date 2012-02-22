using System;
using System.Windows.Input;
using ecologylab.interactive.Utils;

namespace ecologylab.interactive.Commands
{
    public class ExpandElement : ICommand, ILabelledCommand
    {
        public String GetLabel()
        {
            return "Collapse";
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
            Console.WriteLine("Executing CollapseWikiView command");
        }
    }
}