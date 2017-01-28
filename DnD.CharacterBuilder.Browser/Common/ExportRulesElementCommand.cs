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
            //make the list just contain the names of the rules, iterate thru them, looking them up in the dictionary, and adding them if they are there

            var rvstring = "!power {{\n --format|dnd4e\n";

            rulesNeeded.Add("Action Type");
            rulesNeeded.Add("Attack Type");
            rulesNeeded.Add("Keywords");
            rulesNeeded.Add("Power Usage");
            rulesNeeded.Add("Attack");
            rulesNeeded.Add("Target");
            rulesNeeded.Add("Hit");
            rulesNeeded.Add("Effect");
            rulesNeeded.Add("Miss");

            var specificRuleLookup = rulesElement.SpecificRules.ToDictionary(sr => sr.Name);

            rvstring +=

            rvstring += "--name|";
            rvstring += rulesElement.Name;
            rvstring += "\n";

            rvstring += "--leftsub|";
            if (specificRuleLookup.ContainsKey(rulesNeeded[0]))
            {
                SpecificRule currSR = specificRuleLookup[rulesNeeded[0]];
                rvstring += currSR.Description;
                rvstring += "\n";
            }

            rvstring += "--rightsub|";
            if (specificRuleLookup.ContainsKey(rulesNeeded[1]))
            {
                SpecificRule currSR = specificRuleLookup[rulesNeeded[1]];
                rvstring += currSR.Description;
                rvstring += "\n";
            }

            rvstring += "--Flavor|";
            rvstring += rulesElement.FlavorText;
            rvstring += "\n";

            for (var i = 2; i < rulesNeeded.Count; i++)
            {
                if (specificRuleLookup.ContainsKey(rulesNeeded[i]))
                {
                    SpecificRule currSR = specificRuleLookup[rulesNeeded[i]];
                    rvstring += "--";
                    rvstring += currSR.Name;
                    rvstring += "|";
                    rvstring += currSR.Description;
                    rvstring += "\n";
                    if (currSR.Name == "Attack")
                    {
                        rvstring += "--Roll|[[1d20]]\n";
                    }
                }
            }

            /*
            Extract the player's relevant modifier name from the attack specific rule string
            var firstSpace = 0;
            rulesNeeded[4] = element.Description;
            rulesNeeded[9] = element.Description;
            rulesNeeded[9] = rulesNeeded[9].Remove(0, 1);
            firstSpace = rulesNeeded[9].IndexOf(' ');
            rulesNeeded[9] = rulesNeeded[9].Remove(firstSpace, rulesNeeded[9].Length - firstSpace);
            */
                       
            rvstring += "}}";
            Clipboard.SetText(rvstring);
        }
    }
}