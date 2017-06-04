using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class TargetExportParser : SpecificRuleExportParser
    {
        public override string ParseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");

            var description = RegexChecker(rule.Description);
            formatRule.Append(description);

            formatRule.Append("\n");

           
            

            return formatRule.ToString();
        }
        public override string SpecificRuleName
        {
            get
            {
                return "Target";
            }
        }
    }
}