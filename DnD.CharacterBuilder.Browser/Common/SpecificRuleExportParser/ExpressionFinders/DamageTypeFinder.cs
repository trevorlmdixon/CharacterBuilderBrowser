using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnD.CharacterBuilder.Browser.Common
{
    class DamageTypeFinder : ExpressionFinder
    {
        public override void findExpression(string input)
        {
            var formatTypes = new StringBuilder();
            var allDamageTypes = new List<string>();
            var typesFound = new List<string>();

            allDamageTypes.Add("acid");
            allDamageTypes.Add("cold");
            allDamageTypes.Add("fire");
            allDamageTypes.Add("force");
            allDamageTypes.Add("lightning");
            allDamageTypes.Add("necrotic");
            allDamageTypes.Add("poison");
            allDamageTypes.Add("psychic");
            allDamageTypes.Add("radiant");
            allDamageTypes.Add("thunder");

            foreach(var type in allDamageTypes)
            {
                if (input.Contains(type))
                {
                    typesFound.Add(type);
                }
            }

            if (typesFound.Count == 0)
            {
                return;
            }

            for (int i=0;i<typesFound.Count;i++)
            {
                if(i == 0)
                {
                    formatTypes.Append(' ');
                    formatTypes.Append(typesFound[i]);
                }
                else
                {
                    formatTypes.Append(" and ");
                    formatTypes.Append(typesFound[i]);
                }
            }

            this.conString = formatTypes.ToString();
        }

        public override int order
        {
            get
            {
                return 3;
            }
        }
    }
}
