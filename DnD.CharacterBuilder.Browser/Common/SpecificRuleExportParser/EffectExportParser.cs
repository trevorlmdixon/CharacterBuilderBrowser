using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class EffectExportParser : SpecificRuleExportParser
    {
        public override string SpecificRuleName
        {
            get
            {
                return "Effect";
            }
        }

    }
}
