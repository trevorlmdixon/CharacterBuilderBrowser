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
            var rulesElement = parameter as RulesElement;
            if(rulesElement != null && rulesElement.ElementType == "Power")
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            //Clipboard.SetText("Test");
            var rulesElement = parameter as RulesElement;
            List<string> rulesNeeded = new List<string>();
            //[0]ActionType [1]AttackType [2] Keywords [3]Usage [4]Attack(what vs what) [5]Target [6]Hit [7]Effect [8]Miss

            var rvstring = "!power --format|dnd4e";

            for (var i= 0; i < 9; i++)
            {
                rulesNeeded.Add(null);
            }

            //Go through the specific rules, saving any ones that we plan to use into a predetermined index in rulesNeeded
            foreach (SpecificRule element in rulesElement.SpecificRules)
            {
                if (element.Name == "Action Type")
                {
                    rulesNeeded[0] = element.Description;
                    continue;
                }

                if (element.Name == "Attack Type")
                {
                    rulesNeeded[1] = element.Description;
                    continue;
                }

                if (element.Name == "Keywords")
                {
                    rulesNeeded[2] = element.Description;
                    continue;
                }

                if (element.Name == "Power Usage")
                {
                    rulesNeeded[3] = element.Description;
                    continue;
                }

                if (element.Name == "Attack")
                {
                    rulesNeeded[4] = element.Description;
                    continue;
                }

                if (element.Name == "Target")
                {
                    rulesNeeded[5] = element.Description;
                    continue;
                }

                if (element.Name == "Hit")
                {
                    rulesNeeded[6] = element.Description;
                    continue;
                }

                if (element.Name == "Effect")
                {
                    rulesNeeded[7] = element.Description;
                    continue;
                }

                if(element.Name == "Miss")
                {
                    rulesNeeded[8] = element.Description;
                    continue;
                }
            }


            //Format the export string itself, checking to make sure each field is present before concatenating it with the formatted string
            if (rulesElement.Name != null)
            {
                rvstring += " --name|";
                rvstring += rulesElement.Name;
            }

            if (rulesNeeded[0] != null)
            {
                rvstring += " --leftsub|";
                rvstring += rulesNeeded[0];
            }

            if (rulesNeeded[1] != null)
            {
                rvstring += " --rightsub|";
                rvstring += rulesNeeded[1];
            }

            if (rulesNeeded[2] != null)
            {
                rvstring += " --Keywords|";
                rvstring += rulesNeeded[2];
            }

            //if (rulesElement.Source != null)
            //{
            //    rvstring += "\r\n Source: ";
            //    rvstring += rulesElement.Source;
            //}

            if (rulesNeeded[3] != null)
            {
                rvstring += " --Usage|";
                rvstring += rulesNeeded[3];
            }

            if (rulesNeeded[5] != null)
            {
                rvstring += " --Target|";
                rvstring += rulesNeeded[5];
            }

            if (rulesElement.FlavorText != null)
            {
                rvstring += " --Flavor|";
                rvstring += rulesElement.FlavorText;
            }

            //if (rulesElement.Prerequisites != null)
            //{
            //    rvstring += " --Prereq|";
            //    rvstring += rulesElement.Prerequisites;
            //}

            if (rulesNeeded[4] != null)
            {
                rvstring += " --Attack|";
                rvstring += rulesNeeded[4];
            }

            if (rulesNeeded[6] != null)
            {
                rvstring += " --Hit|";
                rvstring += rulesNeeded[6];
            }

            if (rulesNeeded[7] != null)
            {
                rvstring += " --Effect|";
                rvstring += rulesNeeded[7];
            }

            if (rulesNeeded[8] != null)
            {
                rvstring += " --Miss|";
                rvstring += rulesNeeded[8];
            }

            Clipboard.SetText(rvstring);
        }
    }
}