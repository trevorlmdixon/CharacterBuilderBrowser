using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class PowerUsageExportParser : SpecificRuleExportParser
    {
        public override string ParseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();

            /*
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");
            */

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

        public override string SpecificRuleName
        {
            get
            {
                return "Power Usage";
            }
        }
    }
}
