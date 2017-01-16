using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DnD.CharacterBuilder.Browser
{
    public class ExportRulesElementCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //var rulesElement = parameter as RulesElement;
            //if(rulesElement != null && rulesElement.ElementType == "Power")
            //{
                return true;
            //}
            //return false;
        }

        public void Execute(object parameter)
        {
            Clipboard.SetText("Test");
            //Cast as rules element
            //inspect properties
            //pull out data you need
            //put it on the clipboard
        }
    }
}
