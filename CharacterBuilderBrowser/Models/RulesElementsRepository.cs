using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace CharacterBuilderBrowser
{
	public class RulesElementsRepository:IDisposable
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

		private Lucene.Net.Store.Directory rulesElementIndex;

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

							rulesElementIndex=new RAMDirectory();
							using(var indexWriter=new IndexWriter(rulesElementIndex,new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30),IndexWriter.MaxFieldLength.LIMITED))
							{
								foreach(var element in rules.RulesElements)
								{
									indexWriter.Add(element);
								}
								indexWriter.Optimize();
							}
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

		public void Dispose()
		{
			rulesElementIndex.Dispose();
		}

		public ISet<string> Search(string searchText)
		{
			using(var searcher=new IndexSearcher(rulesElementIndex))
			{
				QueryParser parser;
				using(var reader=IndexReader.Open(rulesElementIndex,true))
				{
					parser=new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30,reader.GetFieldNames(IndexReader.FieldOption.ALL).ToArray(),new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
				}
				var result=searcher.Search<RulesElement>(parser.Parse(searchText),1000);
				var elements=result.ScoreDocs.Select(sd => searcher.Doc(sd.Doc).ToObject<RulesElement>().Id);
				return new HashSet<string>(elements);
			}
		}
	}
}
