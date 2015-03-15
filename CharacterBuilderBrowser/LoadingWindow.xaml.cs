using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CharacterBuilderBrowser
{
	/// <summary>
	/// Interaction logic for LoadingWindow.xaml
	/// </summary>
	public partial class LoadingWindow:Window
	{
		public LoadingWindow()
		{
			InitializeComponent();
			this.Loaded+=LoadingWindow_Loaded;
		}

		private async void LoadingWindow_Loaded(object sender,RoutedEventArgs e)
		{
			this.Show();
			await ((Browser)Application.Current).Initialize(this);
			this.Close();
		}

		public void SetStatusText(string text)
		{
			StatusText.Content=text;
		}
	}
}
