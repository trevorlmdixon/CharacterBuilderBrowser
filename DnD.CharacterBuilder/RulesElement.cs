using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CharacterBuilderBrowser
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

	public class RulesElement
	{
		private Collection<SpecificRule> specificRules=new Collection<SpecificRule>();

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("type")]
		public string ElementType { get; set; }

		[XmlAttribute("internal-id")]
		public string Id { get; set; }

		[XmlAttribute("source")]
		public string Source { get; set; }

		[XmlAttribute("revision-date")]
		public string LastUpdated { get; set; }

		[XmlElement("Prereqs")]
		public string Prerequisites { get; set; }

		[XmlElement("Flavor")]
		public string FlavorText { get; set; }

		[XmlElement("specific")]
		public Collection<SpecificRule> SpecificRules { get { return specificRules; } }

		[XmlText]
		public string Description { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			var element=obj as RulesElement;
			if(element!=null)
			{
				return element.Id==this.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}

	public class SpecificRule
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlText]
		public string Description { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
