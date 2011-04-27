using System;
using System.Windows.Input;
using ecologylab.interactive;
using ecologylab.interactive.Utils;

namespace ecologylab.semantics.interactive.Commands
{
    public class RaiseMenuCommand : ICommand, ILabelledCommand
    {

        public void Execute(object parameter)
        {
            new RightHandedControlMenu((CommandParameters)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public string GetLabel()
        {
            return "Menu2";
        }
    }
}