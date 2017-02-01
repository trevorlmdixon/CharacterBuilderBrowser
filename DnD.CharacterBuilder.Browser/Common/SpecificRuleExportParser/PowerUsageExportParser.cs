using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class PowerUsageExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 4;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");

            formatRule.Append("--format|");
            if(rule.Description[1] == 'A') {
                formatRule.Append("at-will");
            }
            if (rule.Description[1] == 'E')
            {
                formatRule.Append("encounter");
            }
            if (rule.Description[1] == 'D')
            {
                formatRule.Append("daily");
            }
            formatRule.Append("\n");


            return formatRule.ToString();
        }

        public override string specificRuleName
        {
            get
            {
                return "Power Usage";
            }
        }
    }
}
