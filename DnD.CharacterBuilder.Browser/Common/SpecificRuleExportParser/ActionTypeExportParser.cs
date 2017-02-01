using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class ActionTypeExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 1;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("--leftsub|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");

            return formatRule.ToString();
        }

        public override string specificRuleName
        {
            get
            {
                return "Action Type";
            }
        }
    }
}
