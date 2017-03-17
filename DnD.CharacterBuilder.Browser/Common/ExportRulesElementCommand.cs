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

            exportBuilder.Append("--corners|10\n}}");
            Clipboard.SetText(exportBuilder.ToString());
        }
    }
}