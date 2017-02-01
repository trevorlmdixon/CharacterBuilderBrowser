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
            var rulesElement = parameter as RulesElement;
            var specificRuleLookup = rulesElement.SpecificRules.ToDictionary(sr => sr.Name);
            var exportBuilder = new StringBuilder();

            var parserTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t != typeof(SpecificRuleExportParser) && typeof(SpecificRuleExportParser).IsAssignableFrom(t));

            exportBuilder.Append("!power {{\n --format|dnd4e\n");
            
            exportBuilder.Append("--name|");
            exportBuilder.Append(rulesElement.Name);
            exportBuilder.Append("\n");

            exportBuilder.Append("--!Tag|");
            exportBuilder.Append(rulesElement.FlavorText);
            exportBuilder.Append("\n");


            foreach (var parser in parserTypes.Select(t => Activator.CreateInstance(t) as SpecificRuleExportParser).OrderBy(t => t.order))
            {
                if (specificRuleLookup.ContainsKey(parser.specificRuleName))
                {
                    SpecificRule currSR = specificRuleLookup[parser.specificRuleName];
                    var result = parser.parseSpecificRule(currSR);
                    exportBuilder.Append(result);
                }
            }

            exportBuilder.Append("--corners|10\n}}");
            Clipboard.SetText(exportBuilder.ToString());
        }
    }
}