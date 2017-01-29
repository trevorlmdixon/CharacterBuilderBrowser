using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class AttackTypeExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 2;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            string rvstring = "";
            rvstring += "--rightsub|";
            rvstring += rule.Description;
            rvstring += "\n";
            return rvstring;
        }

        public override string specificRuleName
        {
            get
            {
                return "Attack Type";
            }
        }

    }
}
