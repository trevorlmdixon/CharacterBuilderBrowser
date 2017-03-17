using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class AttackExportParser : SpecificRuleExportParser
    {
        public override string ParseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();

            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");

            /*
            var abilities = rule.Description.TrimStart(' ');
            ability = ability.Substring(0, ability.IndexOf(' '));
            */

            formatRule.Append("--Attack Roll:|");
            formatRule.Append("[[1d20]] + ");

            var abilities = rule.Description.Split(' ');
            var i = 1;
            while (abilities[i] != "vs.") {
                if (abilities[i] != "or")
                {
                    formatRule.Append("[[@{");
                    formatRule.Append(abilities[i]);
                    formatRule.Append("-mod}]](" + abilities[i].Substring(0,3).ToUpper() + ")");
                    if(abilities[i+1] != "vs.")
                    {
                        formatRule.Append(" or ");
                    }
                }
                i++;
            }
            formatRule.Append(" + [[@{weapon-1-prof}]](Prof)\n");

            return formatRule.ToString();
        }

        public override string SpecificRuleName
        {
            get
            {
                return "Attack";
            }
        }
    }
}