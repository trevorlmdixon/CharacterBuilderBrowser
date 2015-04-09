using System.Collections.Generic;

namespace DnD.CharacterBuilder
{
	public interface IRulesElementRepository
	{
		IEnumerable<RulesElement> AllElements { get; }
		RulesElement Get(string id);
	}
}
