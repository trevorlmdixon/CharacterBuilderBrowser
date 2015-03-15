using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace CharacterBuilderBrowser
{
	public partial class MainWindow:Window
	{
		public RulesElementsRepository Repository { get { return ((Browser)Application.Current).Repository; } }

		public ListCollectionView RulesElementsView { get; private set; }

		private RulesElementSeacher searcher;
		private IDictionary<RulesElement,int> searchResults;
		private string filterText;

		public MainWindow(RulesElementSeacher searcher)
		{
			RulesElementsView=CollectionViewSource.GetDefaultView(Repository.AllElements) as ListCollectionView;
			this.searcher=searcher;
			InitializeComponent();
		}

		public string FilterText
		{
			get { return filterText; }
			set
			{
				if(filterText==value) return;
				filterText=value;
				if(string.IsNullOrWhiteSpace(FilterText))
				{
					RulesElementsView.Filter=null;
					RulesElementsView.CustomSort=null;
				}
				else
				{
					searchResults=searcher.Search(FilterText);
					RulesElementsView.Filter=FilterElements;
					RulesElementsView.CustomSort=new RulesElementScoreSorter(searchResults);
				}
			}
		}

		private void ApplyElementsFilter(object sender,TextChangedEventArgs e)
		{
			if(string.IsNullOrWhiteSpace(FilterText))
			{
				RulesElementsView.Filter=null;
				RulesElementsView.CustomSort=null;
			}
			else
			{
				searchResults=searcher.Search(FilterText);
				RulesElementsView.Filter=FilterElements;
				RulesElementsView.CustomSort=new RulesElementScoreSorter(searchResults);
			}
		}

		private bool FilterElements(object obj)
		{
			var element=obj as RulesElement;
			if(element==null) return false;
			return searchResults.ContainsKey(element);
		}

		private void ViewElementDetails(object sender,MouseButtonEventArgs e)
		{
			var grid=e.Source as DataGrid;
			if(grid==null) return;
			var element=grid.SelectedItem as RulesElement;
			if(element==null) return;
			RulesElementDetailsWindow.Show(element);
		}

		private class RulesElementScoreSorter:IComparer
		{
			private IDictionary<RulesElement,int> scores;

			public RulesElementScoreSorter(IDictionary<RulesElement,int> scores)
			{
				this.scores=scores;
			}

			public int Compare(object x,object y)
			{
				var elementX=x as RulesElement;
				var elementY=y as RulesElement;
				var xScore=scores.ContainsKey(elementX)?scores[elementX]:0;
				var yScore=scores.ContainsKey(elementY)?scores[elementY]:0;
				return yScore-xScore;
			}
		}
	}
}
