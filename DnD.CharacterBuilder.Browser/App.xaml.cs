using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Unity;

namespace DnD.CharacterBuilder.Browser
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class BrowserApplication:Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var container=new UnityContainer();
			container.RegisterType<IRulesElementRepository>(new ContainerControlledLifetimeManager(),new InjectionFactory(c => FileRulesElementsRepository.Create()));
			container.RegisterType<IRulesElementSearcher>(new ContainerControlledLifetimeManager(),new InjectionFactory(c => LuceneRulesElementSeacher.Create(c.Resolve<IRulesElementRepository>())));
			container.RegisterType<ICommand,ShowRulesElementDetailsWindowCommand>();

			var loadingWindow=new LoadingWindow();
			loadingWindow.Loaded+=async (sender,eventArgs) =>
			{
				loadingWindow.SetStatusText("Loading rule elements...");
				try
				{
					await Task.Factory.StartNew(() => container.Resolve<IRulesElementRepository>());
				}
				catch(IOException)
				{
					ShowErrorAndShutdown();
				}
				catch(ResolutionFailedException)
				{
					ShowErrorAndShutdown();
				}
				loadingWindow.SetStatusText("Creating Lucene Index...");
				await Task.Factory.StartNew(() => container.Resolve<IRulesElementSearcher>());

				var viewModel=container.Resolve<RulesElementsCollectionViewModel>();
				var mainWindow=new BrowserWindow(viewModel);
				this.MainWindow=mainWindow;

				loadingWindow.Close();
				mainWindow.Show();
			};
			loadingWindow.Show();
		}

		private static void ShowErrorAndShutdown()
		{
			MessageBox.Show("Could not load Character Builder data. Program will now exit.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
			Application.Current.Shutdown();
		}
	}
}
