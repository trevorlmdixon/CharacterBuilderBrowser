using System.Xml.Serialization;

namespace DnD.CharacterBuilder
{
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
