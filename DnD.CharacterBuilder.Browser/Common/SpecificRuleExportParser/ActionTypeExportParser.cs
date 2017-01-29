using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class ActionTypeExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 1;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            string rvstring = "";
            rvstring += "--leftsub|";
            rvstring += rule.Description;
            rvstring += "\n";

            return rvstring;
        }

        public override string specificRuleName
        {
            get
            {
                return "Action Type";
            }
        }
    }
}
