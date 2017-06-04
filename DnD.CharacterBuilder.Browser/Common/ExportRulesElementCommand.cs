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
            // var parserTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t != typeof(SpecificRuleExportParser) && typeof(SpecificRuleExportParser).IsAssignableFrom(t));
            // var specificRuleLookup = rulesElement.SpecificRules.ToDictionary(sr => sr.Name);


            //dictionary keyed on specific rule names val is parser
            //iterate through the specific rules, looking up the parser for each specific rule and using it if there is such a parser, otherwise use the generic parser

            var rulesElement = parameter as RulesElement;
            var exportBuilder = new StringBuilder();
            var parserTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(SpecificRuleExportParser).IsAssignableFrom(t)).Select(t => Activator.CreateInstance(t) as SpecificRuleExportParser).ToDictionary(p => p.SpecificRuleName);


            exportBuilder.Append("!power {{\n --format|dnd4e\n");
            
            exportBuilder.Append("--name|");
            exportBuilder.Append(rulesElement.Name);
            exportBuilder.Append("\n");

            exportBuilder.Append("--!Tag|");
            exportBuilder.Append(rulesElement.FlavorText);
            exportBuilder.Append("\n");

            foreach (var specRule in rulesElement.SpecificRules)
            {
                if (parserTypes.ContainsKey(specRule.Name)){
                    var parser = parserTypes[specRule.Name];
                    exportBuilder.Append(parser.ParseSpecificRule(specRule));
                }
                else
                {
                    var parser = parserTypes["Generic"];
                    exportBuilder.Append(parser.ParseSpecificRule(specRule));
                }
            }

            //figure out how many targets can be attacked, given the attack type
            var maxAttackTargets = 1;
            if(exportBuilder.ToString().Contains("Close burst"))
            {
                var indexOfNumber = exportBuilder.ToString().IndexOf("Close burst") + "Close burst".Length + 1;
                var number = (int)Char.GetNumericValue(exportBuilder.ToString()[indexOfNumber]);
                maxAttackTargets = 9 + 3 * (number - 2);
                if (number == 1)
                {
                    maxAttackTargets = 8;
                }
            }
            else if (exportBuilder.ToString().Contains("Close blast"))
            {
                var indexOfNumber = exportBuilder.ToString().IndexOf("Close blast") + "Close blast".Length + 1;
                var number = (int)Char.GetNumericValue(exportBuilder.ToString()[indexOfNumber]);
                if (number <= 3)
                {
                    maxAttackTargets = (int)Math.Pow(number, 2);
                }
                else
                {
                    maxAttackTargets = 9 + ((number - 3) * 2);
                }
            }
            else if (exportBuilder.ToString().Contains("Area burst"))
            {
                var indexOfNumber = exportBuilder.ToString().IndexOf("Area burst") + "Area burst".Length + 1;
                var number = (int)Char.GetNumericValue(exportBuilder.ToString()[indexOfNumber]);
                maxAttackTargets = 9 + 3 * (number - 2);
                if (number == 1)
                {
                    maxAttackTargets = 8;
                }
            }
            else if (exportBuilder.ToString().Contains("Area wall"))
            {
                var indexOfNumber = exportBuilder.ToString().IndexOf("Area wall") + "Area wall".Length + 1;
                var number = (int)Char.GetNumericValue(exportBuilder.ToString()[indexOfNumber]);
                maxAttackTargets = number;
            }
            else
            {
                var currString = exportBuilder.ToString();
                var indexOfTargetText = exportBuilder.ToString().IndexOf("--Target:|") + "--Target:|".Length + 1;
                if (indexOfTargetText == -1)
                {
                    indexOfTargetText = exportBuilder.ToString().IndexOf("--Targets:|");
                }
                if (indexOfTargetText != -1)
                {
                    indexOfTargetText += 
                    var lengthOfTargetText = exportBuilder.ToString().IndexOf("\n", indexOfTargetText) - indexOfTargetText - 1;
                    var targetText = exportBuilder.ToString().Substring(indexOfTargetText, lengthOfTargetText);
                    var hey = 0;
                }

                
            }


            var numberList = new List<String>();
            numberList.Add("1st");
            numberList.Add("2nd");
            numberList.Add("3rd");
            numberList.Add("4th");
            numberList.Add("5th");
            numberList.Add("6th");
            numberList.Add("7th");
            numberList.Add("8th");
            numberList.Add("9th");

            var indexInNumList = 0;
            /*
            exportBuilder.

            formatRule.Append("--target_list|");
            formatRule.Append("@{target|");
            formatRule.Append(numberList[indexInNumList++]);
            formatRule.Append("|token_id}");
            */
            exportBuilder.Append("--corners|10\n}}");

            Clipboard.SetText(exportBuilder.ToString());
        }
    }
}