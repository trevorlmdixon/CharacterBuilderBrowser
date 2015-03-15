using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterBuilderBrowser
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class Browser:Application
	{
		private RulesElementsRepository repository;
		public RulesElementsRepository Repository { get { return repository; } }

		internal async Task Initialize(LoadingWindow loadingWindow)
		{
			try
			{
				loadingWindow.SetStatusText("Loading rule elements...");
				await Task.Factory.StartNew(() => repository=RulesElementsRepository.Create());
			}
			catch(IOException)
			{
				ShowErrorAndShutdown();
			}
			catch(InvalidOperationException)
			{
				ShowErrorAndShutdown();
			}

			loadingWindow.SetStatusText("Creating Lucene Index...");
			RulesElementSeacher searcher=null;
			await Task.Factory.StartNew(() => searcher=RulesElementSeacher.Create(repository));

			var mainWindow=new MainWindow(searcher);
			this.MainWindow=mainWindow;
			mainWindow.Show();
		}

		private void ShowErrorAndShutdown()
		{
			MessageBox.Show("Could not load Character Builder data. Program will now exit.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
			Application.Current.Shutdown();
		}
	}
}
