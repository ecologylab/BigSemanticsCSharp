using System;
using System.Windows.Input;
using ecologylab.interactive.Utils;

namespace ecologylab.interactive.Commands
{
    public class MagnifyElementCommand : ICommand, ILabelledCommand
    {
        public MagnifyElementCommand()
        {

        }
        public String GetLabel()
        {
            return "Magnify";
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
            Console.WriteLine("Executing Magnify command");
        }
    }
}