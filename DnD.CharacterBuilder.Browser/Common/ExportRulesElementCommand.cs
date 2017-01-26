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
            //[0]ActionType [1]AttackType [2] Keywords [3]Usage [4]Attack(what vs what) [5]Target [6]Hit [7]Effect [8]Miss [9]Modifier

            var rvstring = "!power {{\n --format|dnd4e\n";
            var firstSpace = 0;

            for (var i= 0; i < 10; i++)
            {
                rulesNeeded.Add(null);
            }

            //Go through the specific rules, saving any ones that we plan to use into a predetermined index in rulesNeeded
            foreach (SpecificRule element in rulesElement.SpecificRules)
            {
                switch (element.Name)
                {
                    case "Action Type":
                        rulesNeeded[0] = element.Description;
                        break;
                    case "Attack Type":
                        rulesNeeded[1] = element.Description;
                        break;
                    case "Keywords":
                        rulesNeeded[2] = element.Description;
                        break;
                    case "Power Usage":
                        rulesNeeded[3] = element.Description;
                        break;
                    case "Attack":
                        rulesNeeded[4] = element.Description;
                        //Extract the player's relevant modifier name from the attack specific rule string
                        rulesNeeded[9] = element.Description;
                        rulesNeeded[9] = rulesNeeded[9].Remove(0, 1);
                        firstSpace = rulesNeeded[9].IndexOf(' ');
                        rulesNeeded[9] = rulesNeeded[9].Remove(firstSpace, rulesNeeded[9].Length - firstSpace);
                        break;
                    case "Target":
                        rulesNeeded[5] = element.Description;
                        break;
                    case "Hit":
                        rulesNeeded[6] = element.Description;
                        break;
                    case "Effect":
                        rulesNeeded[7] = element.Description;
                        break;
                    case "Miss":
                        rulesNeeded[8] = element.Description;
                        break;
                }
            }

            //Format the export string itself, checking to make sure each field is present before concatenating it with the formatted string
            if (rulesElement.Name != null)
            {
                rvstring += " --name|";
                rvstring += rulesElement.Name;
                rvstring += "\n";
            }

            if (rulesNeeded[0] != null)
            {
                rvstring += " --leftsub|";
                rvstring += rulesNeeded[0];
                rvstring += "\n";
            }

            if (rulesNeeded[1] != null)
            {
                rvstring += " --rightsub|";
                rvstring += rulesNeeded[1];
                rvstring += "\n";
            }

            if (rulesNeeded[2] != null)
            {
                rvstring += " --Keywords|";
                rvstring += rulesNeeded[2];
                rvstring += "\n";
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
                rvstring += "\n";
            }

            if (rulesNeeded[5] != null)
            {
                rvstring += " --Target|";
                rvstring += rulesNeeded[5];
                rvstring += "\n";
            }

            if (rulesElement.FlavorText != null)
            {
                rvstring += " --Flavor|";
                rvstring += rulesElement.FlavorText;
                rvstring += "\n";
            }

            //if (rulesElement.Prerequisites != null)
            //{
            //    rvstring += " --Prereq|";
            //    rvstring += rulesElement.Prerequisites;
            //    rvstring += "\n";
            //}

            if (rulesNeeded[4] != null)
            {
                rvstring += " --Attack|";
                rvstring += rulesNeeded[4];
                rvstring += "\n";
                rvstring += "--Roll|[[1d20+@{";
                rvstring += rulesNeeded[9];
                rvstring += "-Mod}]]\n";
            }

            if (rulesNeeded[6] != null)
            {
                rvstring += " --Hit|";
                rvstring += rulesNeeded[6];
                rvstring += "\n";
            }

            if (rulesNeeded[7] != null)
            {
                rvstring += " --Effect|";
                rvstring += rulesNeeded[7];
                rvstring += "\n";
            }

            if (rulesNeeded[8] != null)
            {
                rvstring += " --Miss|";
                rvstring += rulesNeeded[8];
                rvstring += "\n";
            }

            rvstring += "}}";
            Clipboard.SetText(rvstring);
        }
    }
}