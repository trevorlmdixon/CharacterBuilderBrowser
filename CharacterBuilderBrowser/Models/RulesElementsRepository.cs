using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace CharacterBuilderBrowser
{
	public class RulesElementsRepository
	{
		private string filePath;
		private D20Rules rules;
		private Dictionary<string,RulesElement> allElementsDictionary;

		public IEnumerable<RulesElement> AllElements { get { return rules.RulesElements; } }

		private RulesElementsRepository(string filePath)
		{
			this.filePath=filePath;
		}

		public static RulesElementsRepository Create()
		{
			return Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"CBLoader\\combined.dnd40"));
		}

		public static RulesElementsRepository Create(string filePath)
		{
			if(!File.Exists(filePath)) throw new InvalidOperationException("File does not exist.");

			var repository=new RulesElementsRepository(filePath);
			repository.Load();
			return repository;
		}

		private void Load()
		{
			using(var stream=File.OpenRead(filePath))
			{
				using(var xmlReader=XmlReader.Create(stream))
				{
					var xs=new XmlSerializer(typeof(D20Rules));
					rules=(D20Rules)xs.Deserialize(xmlReader);
					allElementsDictionary=rules.RulesElements.ToDictionary(e => e.Id);
				}
			}
		}

		public RulesElement GetRulesElement(string id)
		{
			return allElementsDictionary.ContainsKey(id)?allElementsDictionary[id]:null;
		}
	}
}
