using System.Windows;

namespace DnD.CharacterBuilder.Browser
{
	public partial class BrowserWindow:Window
	{
		public BrowserWindow(RulesElementsCollectionViewModel model)
		{
			this.DataContext=model;
			InitializeComponent();
		}
	}
}
