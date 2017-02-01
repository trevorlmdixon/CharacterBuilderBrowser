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
            var formatDamage = new StringBuilder();
            var hitText = rule.Description;

            if (hitText.Contains('\n'))
            {
                hitText = hitText.Substring(0, rule.Description.IndexOf("\n"));
            }

            Match damageExpression = Regex.Match(hitText, @"\d.*damage");

            if(damageExpression.Success == true)
            {
                var damageText = damageExpression.Value;
                hitText = hitText.Remove(hitText.IndexOf(damageText), damageText.Length);

                var expressionFinders = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t != typeof(ExpressionFinder) && typeof(ExpressionFinder).IsAssignableFrom(t));

                formatDamage.Append("[[");
                foreach (var finder in expressionFinders.Select(t => Activator.CreateInstance(t) as ExpressionFinder).OrderBy(t => t.order))
                {
                    finder.findExpression(damageText);
                    if (finder.convertedString() != null)
                    {
                        formatDamage.Append(finder.convertedString());
                    }
                }
                formatDamage.Append(" damage");
            }

            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(formatDamage);
            formatRule.Append(hitText);
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