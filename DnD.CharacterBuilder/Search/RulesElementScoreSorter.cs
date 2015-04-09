using System.Collections;
using System.Collections.Generic;

namespace DnD.CharacterBuilder
{
	public class RulesElementScoreSorter:IComparer
	{
		private IDictionary<RulesElement,int> scores;

		public RulesElementScoreSorter(IDictionary<RulesElement,int> scores)
		{
			this.scores=scores;
		}

		public int Compare(object x,object y)
		{
			var elementX=x as RulesElement;
			var elementY=y as RulesElement;
			var xScore=scores.ContainsKey(elementX)?scores[elementX]:0;
			var yScore=scores.ContainsKey(elementY)?scores[elementY]:0;
			return yScore-xScore;
		}
	}
}
