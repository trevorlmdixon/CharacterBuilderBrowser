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
            var specificRuleLookup = rulesElement.SpecificRules.ToDictionary(sr => sr.Name);
            var exportBuilder = new StringBuilder();
            //var parserTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(SpecificRuleExportParser)));

            var parserTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t != typeof(SpecificRuleExportParser) && typeof(SpecificRuleExportParser).IsAssignableFrom(t));

            exportBuilder.Append("!power {{\n --format|dnd4e\n");
            
            /*
                        rulesNeeded.Add("Action Type");
                        rulesNeeded.Add("Attack Type");
                        rulesNeeded.Add("Keywords");
                        rulesNeeded.Add("Power Usage");
                        rulesNeeded.Add("Attack");
                        rulesNeeded.Add("Target");
                        rulesNeeded.Add("Hit");
                        rulesNeeded.Add("Effect");
                        rulesNeeded.Add("Miss");
            */

            exportBuilder.Append("--name|");
            exportBuilder.Append(rulesElement.Name);
            exportBuilder.Append("\n");

            exportBuilder.Append("--Flavor|");
            exportBuilder.Append(rulesElement.FlavorText);
            exportBuilder.Append("\n");

            /*
            exportBuilder.Append("--leftsub|";
            if (specificRuleLookup.ContainsKey(rulesNeeded[0]))
            {
                SpecificRule currSR = specificRuleLookup[rulesNeeded[0]];
                exportBuilder.Append(currSR.Description;
                exportBuilder.Append("\n";
            }

            exportBuilder.Append("--rightsub|";
            if (specificRuleLookup.ContainsKey(rulesNeeded[1]))
            {
                SpecificRule currSR = specificRuleLookup[rulesNeeded[1]];
                exportBuilder.Append(currSR.Description;
                exportBuilder.Append("\n";
            }
            */

            foreach (var parser in parserTypes.Select(t => Activator.CreateInstance(t) as SpecificRuleExportParser).OrderBy(t => t.order))
            {
                if (specificRuleLookup.ContainsKey(parser.specificRuleName))
                {
                    SpecificRule currSR = specificRuleLookup[parser.specificRuleName];
                    var result = parser.parseSpecificRule(currSR);
                    exportBuilder.Append(result);
                }
            }

            /*
                            for (var i = 2; i < rulesNeeded.Count; i++)
                        {
                            if (specificRuleLookup.ContainsKey(rulesNeeded[i]))
                            {
                                SpecificRule currSR = specificRuleLookup[rulesNeeded[i]];
                                exportBuilder.Append("--";
                                exportBuilder.Append(currSR.Name;
                                exportBuilder.Append("|";
                                exportBuilder.Append(currSR.Description;
                                exportBuilder.Append("\n";
                                if (currSR.Name == "Attack")
                                {
                                    exportBuilder.Append("--Roll|[[1d20";
                                    exportBuilder.Append("]]\n";
                                }
                            }
                        }
            */
            /*
            Extract the player's relevant modifier name from the attack specific rule string
            var firstSpace = 0;
            rulesNeeded[4] = element.Description;
            rulesNeeded[9] = element.Description;
            rulesNeeded[9] = rulesNeeded[9].Remove(0, 1);
            firstSpace = rulesNeeded[9].IndexOf(' ');
            rulesNeeded[9] = rulesNeeded[9].Remove(firstSpace, rulesNeeded[9].Length - firstSpace);
            */

            exportBuilder.Append("}}");
            Clipboard.SetText(exportBuilder.ToString());
        }
    }
}