using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class KeywordsExportParser : SpecificRuleExportParser
    {
        public override int order
        {
            get
            {
                return 3;
            }
        }

        public override string specificRuleName
        {
            get
            {
                return "Keywords";
            }
        }
    }
}
