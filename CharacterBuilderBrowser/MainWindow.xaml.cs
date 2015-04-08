using System.Windows;

namespace CharacterBuilderBrowser
{
	public partial class MainWindow:Window
	{
		public MainWindow(RulesElementsCollectionViewModel model)
		{
			this.DataContext=model;
			InitializeComponent();
		}
	}
}
