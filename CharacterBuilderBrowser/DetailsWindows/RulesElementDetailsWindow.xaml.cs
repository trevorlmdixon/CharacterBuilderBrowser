using System.Windows;

namespace CharacterBuilderBrowser
{
	public partial class RulesElementDetailsWindow:Window
	{
		public RulesElementDetailsWindow(RulesElementDetailsViewModel model)
		{
			this.DataContext=model;
			InitializeComponent();
		}
	}
}
