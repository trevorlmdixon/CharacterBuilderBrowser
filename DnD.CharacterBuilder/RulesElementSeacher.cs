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
	public interface IRulesElementSearcher:IDisposable
	{
		void Index(IRulesElementRepository repository);
		IDictionary<RulesElement,int> Search(string searchText);
	}

	public class LuceneRulesElementSeacher:IRulesElementSearcher
	{
		private Directory rulesElementIndex;
		private int repositoryCount;

		public LuceneRulesElementSeacher()
		{
			rulesElementIndex=new RAMDirectory();
		}

		public static LuceneRulesElementSeacher Create(IRulesElementRepository repository)
		{
			var searcher=new LuceneRulesElementSeacher();
			searcher.Index(repository);
			return searcher;
		}

		public void Dispose()
		{
			rulesElementIndex.Dispose();
		}

		public void Index(IRulesElementRepository repository)
		{
			using(var indexWriter=new IndexWriter(rulesElementIndex,new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30),IndexWriter.MaxFieldLength.LIMITED))
			{
				foreach(var element in repository.AllElements)
				{
					indexWriter.Add(element);
					repositoryCount++;
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
				var result=searcher.Search<RulesElement>(parser.Parse(searchText),repositoryCount);
				return result.ScoreDocs.ToDictionary(sd => searcher.Doc(sd.Doc).ToObject<RulesElement>(),sd=>(int)(sd.Score*repositoryCount));
			}
		}
	}
}
