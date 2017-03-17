using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DnD.CharacterBuilder.Browser
{
    class HitExportParser : SpecificRuleExportParser
    {
        public override string ParseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();

            var description = this.RegexChecker(rule.Description);
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(description);
            formatRule.Append("\n");
            return formatRule.ToString();
            
        }

        public override string SpecificRuleName
        {
            get
            {
                return "Hit";
            }
        }
    }
}