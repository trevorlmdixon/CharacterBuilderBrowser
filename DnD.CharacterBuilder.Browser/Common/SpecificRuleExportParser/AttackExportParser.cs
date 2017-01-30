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
            string ability;
            string rvstring = "";
            rvstring += "--";
            rvstring += rule.Name;
            rvstring += ":|";
            rvstring += rule.Description;

            ability = rule.Description.TrimStart(' ');
            ability = ability.Substring(0, ability.IndexOf(' '));

            rvstring += "\n";
            rvstring += "^^";
            rvstring += "\n";
            rvstring += "[[1d20 + @{";
            rvstring += ability;
            rvstring += "-mod}]]\n";


            return rvstring;
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