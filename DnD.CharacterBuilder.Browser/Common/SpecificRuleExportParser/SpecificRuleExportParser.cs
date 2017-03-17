using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DnD.CharacterBuilder.Browser
{
    public class SpecificRuleExportParser
    {
        private static List<string> IgnoredSpecRules
        {
            get
            {
                var ignoredSpecRules = new List<string>();
                ignoredSpecRules.Add("Class");
                ignoredSpecRules.Add("Power Type");
                ignoredSpecRules.Add("Display");
                ignoredSpecRules.Add("_ParentFeature");
                ignoredSpecRules.Add("_Subclasses");
                ignoredSpecRules.Add("_SkillPower");
                ignoredSpecRules.Add("_AssociatedFeats");
                ignoredSpecRules.Add("Level");
                ignoredSpecRules.Add("_Tags");
                return ignoredSpecRules;
            }
        }

        static protected string RegexChecker(string description)
        {
            if (description.Contains('\n'))
            {
                description = description.Substring(0, description.IndexOf("\n"));
            }

            var dieExpression = Regex.Match(description, @"\dd\d+");
            if (dieExpression.Success == true)
            {
                description = description.Replace(dieExpression.Value, "[[" + dieExpression.Value + "]]");
            }

            var weaponExpression = Regex.Match(description, @"\d\[W\]");
            if (weaponExpression.Success == true)
            {
                var numDice = int.Parse(weaponExpression.Value.Substring(0, 1));
                var weaponDamage = new StringBuilder();
                for (int i = 0; i < numDice; i++)
                {
                    weaponDamage.Append("[[@{weapon-1-num-dice}d@{weapon-1-dice}]]");
                    if (i+1 < numDice)
                    {
                        weaponDamage.Append(" + ");
                    }
                }
                description = description.Replace(weaponExpression.Value, weaponDamage.ToString());
            }

            var modifierExpression = Regex.Match(description, @"\S+ modifier", RegexOptions.IgnoreCase);
            while(modifierExpression.Success == true)
            {
                var ability = modifierExpression.Value.Substring(0, modifierExpression.Value.IndexOf(' '));
                var value = modifierExpression.Value;

                var yourModifierExpression = Regex.Match(description, @"your \S+ modifier", RegexOptions.IgnoreCase);
                if (yourModifierExpression.Success == true && yourModifierExpression.Value.Contains(value))
                {
                    value = yourModifierExpression.Value;
                }
                description = description.Replace(value, "[[@{" + ability + "-mod}]](" + ability.Substring(0,3).ToUpper() + ")");
                modifierExpression = Regex.Match(description, @"\S+ modifier", RegexOptions.IgnoreCase);
            }

            return description;
        }

        public virtual string ParseSpecificRule(SpecificRule rule)
        {
            if (IgnoredSpecRules.Contains(rule.Name) || rule.Description == null)
            {
                return "";
            }
            var formatRule = new StringBuilder();
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");

            var description = RegexChecker(rule.Description);
            formatRule.Append(description);

            formatRule.Append("\n");
            return formatRule.ToString();
        }

        public virtual string SpecificRuleName
        {
            get
            {
                return "Generic";
            }
        }

    }
}

