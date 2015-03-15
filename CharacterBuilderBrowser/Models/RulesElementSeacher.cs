using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace CharacterBuilderBrowser
{
	public class RulesElementSeacher:IDisposable
	{
		private RulesElementsRepository repository;
		private Directory rulesElementIndex;

		private RulesElementSeacher(RulesElementsRepository repository)
		{
			this.repository=repository;
		}

		public static RulesElementSeacher Create(RulesElementsRepository repository)
		{
			var searcher=new RulesElementSeacher(repository);
			searcher.Index();
			return searcher;
		}

		public void Dispose()
		{
			rulesElementIndex.Dispose();
		}

		private void Index()
		{
			rulesElementIndex=new RAMDirectory();
			using(var indexWriter=new IndexWriter(rulesElementIndex,new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30),IndexWriter.MaxFieldLength.LIMITED))
			{
				foreach(var element in repository.AllElements)
				{
					indexWriter.Add(element);
				}
				indexWriter.Optimize();
			}
		}

		public IDictionary<RulesElement,int> Search(string searchText)
		{
			using(var searcher=new IndexSearcher(rulesElementIndex))
			{
				QueryParser parser;
				using(var reader=IndexReader.Open(rulesElementIndex,true))
				{
					parser=new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30,reader.GetFieldNames(IndexReader.FieldOption.ALL).ToArray(),new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
				}
				var result=searcher.Search<RulesElement>(parser.Parse(searchText),40000);
				return result.ScoreDocs.ToDictionary(sd => searcher.Doc(sd.Doc).ToObject<RulesElement>(),sd=>(int)(sd.Score*40000));
			}
		}
	}
}
