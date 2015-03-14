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
		#region Singleton

		private static object singletonLock=new object();
		private static RulesElementsRepository instance;
		public static RulesElementsRepository Instance
		{
			get
			{
				if(instance==null)
				{
					lock(singletonLock)
					{
						if(instance==null)
						{
							var temp=new RulesElementsRepository();
							if(temp.Load())
								instance=temp;
						}
					}
				}
				return instance;
			}
		}

		#endregion

		private D20Rules rules;
		public IEnumerable<RulesElement> AllElements { get { return rules.RulesElements; } }

		private Lazy<Dictionary<string,RulesElement>> allElementsDictionary;

		private RulesElementsRepository()
		{
			allElementsDictionary=new Lazy<Dictionary<string,RulesElement>>(() => AllElements.ToDictionary(e => e.Id),LazyThreadSafetyMode.ExecutionAndPublication);
		}

		private bool Load()
		{
			var filePath=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"CBLoader\\combined.dnd40");
			if(!File.Exists(filePath)) return false;

			try
			{
				using(var stream=File.OpenRead(filePath))
				{
					using(var xmlReader=XmlReader.Create(stream))
					{
						var xs=new XmlSerializer(typeof(D20Rules));
						try
						{
							rules=(D20Rules)xs.Deserialize(xmlReader);
						}
						catch(InvalidOperationException)
						{
							return false;
						}
					}
				}
			}
			catch(IOException)
			{
				return false;
			}

			return true;
		}

		public RulesElement GetRulesElement(string id)
		{
			return allElementsDictionary.Value[id];
		}
	}
}
