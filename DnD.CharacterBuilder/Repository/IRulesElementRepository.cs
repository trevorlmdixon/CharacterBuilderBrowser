using System.Collections.Generic;

namespace DnD.CharacterBuilder
{
	public interface IRulesElementRepository
	{
		IEnumerable<RulesElement> AllElements { get; }
		RulesElement GetElement(string id);
	}
}
