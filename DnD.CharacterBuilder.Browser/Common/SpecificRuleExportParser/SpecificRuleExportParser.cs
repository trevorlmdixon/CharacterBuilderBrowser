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
            string rvstring = "";
            rvstring += "--";
            rvstring += rule.Name;
            rvstring += ":|";
            rvstring += rule.Description;
            rvstring += "\n";
            return rvstring;
        }
        public abstract string specificRuleName { get; }
    }
}

