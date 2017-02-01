using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser
{
    public abstract class ExpressionFinder
    {
        protected string conString = null;
        public virtual string convertedString()
        {
            return this.conString;
        }
        public abstract void findExpression(string input);
        public abstract int order { get; }
    }
}
