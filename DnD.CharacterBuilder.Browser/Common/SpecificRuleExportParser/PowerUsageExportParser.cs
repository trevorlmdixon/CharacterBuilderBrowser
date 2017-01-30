using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class PowerUsageExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 4;
            }
        }

        public override string parseSpecificRule(SpecificRule rule)
        {
            string rvstring = "";
            rvstring += "--";
            rvstring += rule.Name;
            rvstring += ":|";
            rvstring += rule.Description;
            rvstring += "\n";

            rvstring += "--format|";
            if(rule.Description[1] == 'A') {
                rvstring += "at-will";
            }
            if (rule.Description[1] == 'E')
            {
                rvstring += "encounter";
            }
            if (rule.Description[1] == 'D')
            {
                rvstring += "daily";
            }
            rvstring += "\n";


            return rvstring;
        }

        public override string specificRuleName
        {
            get
            {
                return "Power Usage";
            }
        }
    }
}
