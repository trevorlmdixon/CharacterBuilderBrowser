using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class AttackExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 5;
            }
        }
       
        public override string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();

            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(rule.Description);

            var ability = rule.Description.TrimStart(' ');
            ability = ability.Substring(0, ability.IndexOf(' '));

            formatRule.Append("\n^^");
            formatRule.Append("[[1d20 + @{");
            formatRule.Append(ability);
            formatRule.Append("-mod} + @{weapon-1-prof}]]\n");


            return formatRule.ToString();
        }

        public override string specificRuleName
        {
            get
            {
                return "Attack";
            }
        }
    }
}