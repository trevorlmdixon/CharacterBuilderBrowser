using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace DnD.CharacterBuilder
{
	[XmlRoot("D20Rules")]
	public class D20Rules
	{
		private Collection<RulesElement> rulesElements=new Collection<RulesElement>();

		[XmlAttribute("game-system")]
		public string GameSystem { get; set; }

		[XmlElement("RulesElement")]
		public Collection<RulesElement> RulesElements { get { return rulesElements; } }

		public override string ToString()
		{
			return GameSystem;
		}
	}
}
