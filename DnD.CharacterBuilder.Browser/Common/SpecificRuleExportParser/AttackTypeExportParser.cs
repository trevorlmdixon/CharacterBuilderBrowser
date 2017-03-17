using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class AttackTypeExportParser : SpecificRuleExportParser
    {
        public override string ParseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("--rightsub|");
            var description = RegexChecker(rule.Description);
            formatRule.Append(description);
            formatRule.Append("\n");
            return formatRule.ToString();
        }

        public override string SpecificRuleName
        {
            get
            {
                return "Attack Type";
            }
        }

    }
}
