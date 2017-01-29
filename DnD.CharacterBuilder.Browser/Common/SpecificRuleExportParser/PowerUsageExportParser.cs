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

        public override string specificRuleName
        {
            get
            {
                return "Power Usage";
            }
        }
    }
}
