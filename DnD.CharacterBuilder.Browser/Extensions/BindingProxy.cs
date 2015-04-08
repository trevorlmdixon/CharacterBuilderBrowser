using System.Windows;

namespace CharacterBuilderBrowser
{
	public class BindingProxy:Freezable
	{
		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		public static readonly DependencyProperty DataContextProperty=DependencyProperty.Register("DataContext",typeof(object),typeof(BindingProxy));

		public object DataContext
		{
			get { return (object)GetValue(DataContextProperty); }
			set { SetValue(DataContextProperty,value); }
		}
	}
}
