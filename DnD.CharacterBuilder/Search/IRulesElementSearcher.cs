using System;
using System.Collections.Generic;

namespace DnD.CharacterBuilder
{
	public interface IRulesElementSearcher:IDisposable
	{
		void Index(IRulesElementRepository repository);
		IDictionary<RulesElement,int> Search(string searchText);
	}
}
