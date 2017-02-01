using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class WeaponFinder : ExpressionFinder
    {
        public override void findExpression(string input)
        {
            Match match = Regex.Match(input, @"\d\[W\]", RegexOptions.IgnoreCase);
            if (match.Success == true)
            {
                this.conString = (match.Value.Substring(0, match.Value.IndexOf('[')) + "d8");
            }
        }

        public override int order
        {
            get
            {
                return 1;
            }
        }
    }
}
