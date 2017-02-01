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
        public override int order
        {
            get
            {
                return 7;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            var hitText = "";

            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");

            if (rule.Description.Contains('\n'))
            {
                hitText = rule.Description.Substring(0, rule.Description.IndexOf("\n"));
            }
            else
            {
                hitText = rule.Description;
            }
            

            Match dieMatch = Regex.Match(hitText, @"\dd\d", RegexOptions.IgnoreCase);
            Match weaponMatch = Regex.Match(hitText, @"\d\[W\]", RegexOptions.IgnoreCase);
            Match modMatch = Regex.Match(hitText, @"\S+ modifier", RegexOptions.IgnoreCase);

            if(dieMatch.Success == true)
            {
                formatRule.Append("[[" + dieMatch.Value);

                if (modMatch.Success == true)
                {
                    var ability = modMatch.Value.Substring(0, modMatch.Value.IndexOf(' '));
                    formatRule.Append(" + @{" + ability + "-mod}");
                }

                formatRule.Append("]]");
            }

            if (weaponMatch.Success == true)
            {
                formatRule.Append("[[" + weaponMatch.Value.Substring(0, weaponMatch.Value.IndexOf('[')) + "d8");

                if (modMatch.Success == true)
                {
                    var ability = modMatch.Value.Substring(0, modMatch.Value.IndexOf(' '));
                    formatRule.Append(" + @{" + ability + "-mod}");
                }

                formatRule.Append("]]");
            }

            formatRule.Append("\n");
            return formatRule.ToString();
            
        }

        public override string specificRuleName
        {
            get
            {
                return "Hit";
            }
        }
    }
}