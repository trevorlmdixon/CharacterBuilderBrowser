using System.Windows;

namespace DnD.CharacterBuilder.Browser
{
	/// <summary>
	/// Interaction logic for LoadingWindow.xaml
	/// </summary>
	public partial class LoadingWindow:Window
	{
		public LoadingWindow()
		{
			InitializeComponent();
		}

		public void SetStatusText(string text)
		{
			StatusText.Content=text;
		}
	}
}
