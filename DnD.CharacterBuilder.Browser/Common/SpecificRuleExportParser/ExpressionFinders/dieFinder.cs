using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    class DieFinder : ExpressionFinder
    {
        public override void findExpression(string input)
        {
            Match match = Regex.Match(input, @"\dd\d", RegexOptions.IgnoreCase);
            if (match.Success == true)
            {
                this.conString = match.Value;
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
