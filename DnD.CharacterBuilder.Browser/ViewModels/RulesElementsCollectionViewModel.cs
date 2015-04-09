using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace DnD.CharacterBuilder.Browser
{
	public class RulesElementsCollectionViewModel
	{
		private IRulesElementSearcher searcher;
		private ListCollectionView view;
		private string filterText;
		private IDictionary<RulesElement,int> searchResults;

		public RulesElementsCollectionViewModel(IRulesElementRepository repository,IRulesElementSearcher searcher,ICommand viewElementCommand)
		{
			if(repository==null) throw new ArgumentNullException("repository");
			if(searcher==null) throw new ArgumentNullException("searcher");
			if(viewElementCommand==null) throw new ArgumentNullException("viewElementCommand");

			this.searcher=searcher;
			this.AllElementsCount=repository.AllElements.Count();
			this.ViewElementCommand=viewElementCommand;
			this.view=new ListCollectionView(repository.AllElements.ToList());
		}

		public int AllElementsCount { get; private set; }
		public ICollectionView RepositoryCollectionView { get { return view; } }
		public ICommand ViewElementCommand { get; private set; }

		public string FilterText
		{
			get { return filterText; }
			set
			{
				if(filterText==value) return;
				filterText=value;
				if(string.IsNullOrWhiteSpace(filterText))
				{
					searchResults=null;
					view.Filter=null;
					view.CustomSort=null;
				}
				else
				{
					searchResults=searcher.Search(filterText);
					view.Filter=FilterElements;
					view.CustomSort=new RulesElementScoreSorter(searchResults);
				}
			}
		}

		private bool FilterElements(object obj)
		{
			var element=obj as RulesElement;
			if(element==null) return false;
			return searchResults.ContainsKey(element);
		}
	}
}
