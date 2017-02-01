using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class DamageModifierFinder : ExpressionFinder
    {
        public override void findExpression(string input)
        {
            Match match = Regex.Match(input, @"\S+ modifier", RegexOptions.IgnoreCase);
            if (match.Success == true)
            {
                var ability = match.Value.Substring(0, match.Value.IndexOf(' '));
                this.conString = " + @{" + ability + "-mod}]]";
            }
            else
            {
                this.conString = "]]";
            }
        }

        public override int order
        {
            get
            {
                return 2;
            }
        }
    }
}
