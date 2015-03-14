using System;
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
		private ICollectionView rulesElementsView;
		public ICollectionView RulesElementsView { get { return rulesElementsView; } }

		private ISet<string> filterIds;
		public string FilterText { get; set; }

		public MainWindow()
		{
			rulesElementsView=CollectionViewSource.GetDefaultView(RulesElementsRepository.Instance.AllElements);
			InitializeComponent();
		}

		private void ApplyElementsFilter(object sender,TextChangedEventArgs e)
		{
			if(string.IsNullOrWhiteSpace(FilterText))
			{
				rulesElementsView.Filter=null;
			}
			else
			{
				filterIds=RulesElementsRepository.Instance.Search(FilterText);
				rulesElementsView.Filter=FilterElements;
			}
		}

		private bool FilterElements(object obj)
		{
			var element=obj as RulesElement;
			if(element==null) return false;
			return filterIds.Contains(element.Id);
		}

		private void ViewElementDetails(object sender,MouseButtonEventArgs e)
		{
			var grid=e.Source as DataGrid;
			if(grid==null) return;
			var element=grid.SelectedItem as RulesElement;
			if(element==null) return;
			var details=new RulesElementDetailsWindow(element);
			details.Show();
		}
	}
}
