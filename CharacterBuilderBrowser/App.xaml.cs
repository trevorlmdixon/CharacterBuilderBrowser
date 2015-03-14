using System.Windows;

namespace CharacterBuilderBrowser
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App:Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			if(RulesElementsRepository.Instance==null)
			{
				MessageBox.Show("Could not load Character Builder data. Program will now exit.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
				Shutdown();
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			RulesElementsRepository.Instance.Dispose();
			base.OnExit(e);
		}
	}
}
