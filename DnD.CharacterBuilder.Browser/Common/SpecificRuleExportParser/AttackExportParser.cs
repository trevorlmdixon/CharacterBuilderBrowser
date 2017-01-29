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
            string rvstring = "";
            rvstring += "--";
            rvstring += rule.Name;
            rvstring += "|";
            rvstring += rule.Description;
            rvstring += "\n";
            rvstring += "--Roll|[[1d20]]\n";

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