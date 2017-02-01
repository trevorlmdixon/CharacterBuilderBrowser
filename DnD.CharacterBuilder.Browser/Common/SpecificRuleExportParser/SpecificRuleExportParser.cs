using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    public abstract class SpecificRuleExportParser
    {
        public abstract int order { get; }
        public virtual string parseSpecificRule(SpecificRule rule)
        {
            var formatRule = new StringBuilder();
            formatRule.Append("");
            formatRule.Append("--");
            formatRule.Append(rule.Name);
            formatRule.Append(":|");
            formatRule.Append(rule.Description);
            formatRule.Append("\n");
            return formatRule.ToString();
        }
        public abstract string specificRuleName { get; }
    }
}

