using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace CharacterBuilderBrowser
{
	public class RulesElementIdHyperlinkConverter:DependencyObject,IValueConverter
	{
		public static readonly DependencyProperty RepositoryProperty=DependencyProperty.Register("Repository",typeof(IRulesElementRepository),typeof(RulesElementIdHyperlinkConverter));
		public static readonly DependencyProperty HyperlinkClickCommandProperty=DependencyProperty.Register("HyperlinkClickCommand",typeof(ICommand),typeof(RulesElementIdHyperlinkConverter));

		public IRulesElementRepository Repository
		{
			get { return (IRulesElementRepository)this.GetValue(RepositoryProperty); }
			set { this.SetValue(RepositoryProperty,value); }
		}

		public ICommand HyperlinkClickCommand
		{
			get { return (ICommand)this.GetValue(HyperlinkClickCommandProperty); }
			set { this.SetValue(HyperlinkClickCommandProperty,value); }
		}

		public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
		{
			if(value==null)
			{
				return null;
			}

			var valueStr=value.ToString().Trim();
			var span=new Span();

			var runs=Regex.Split(valueStr,"(ID_[A-Z0-9'()_-]+)");
			for(int i=0;i<runs.Length;i++)
			{
				var runStr=runs[i];
				if(i%2==1&&Repository!=null)
				{
					var element=Repository.Get(runStr);
					if(element!=null)
					{
						var link=new Hyperlink(new Run(element.Name));
						link.Command=HyperlinkClickCommand;
						link.CommandParameter=element;
						span.Inlines.Add(link);
						continue;
					}
				}
				span.Inlines.Add(new Run(runStr));
			}
			
			return span;
		}

		public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
