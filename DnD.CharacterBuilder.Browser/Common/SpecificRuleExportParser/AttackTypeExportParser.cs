using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class AttackTypeExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 2;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("--rightsub|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");
            return formatRule.ToString();
        }

        public override string specificRuleName
        {
            get
            {
                return "Attack Type";
            }
        }

    }
}
