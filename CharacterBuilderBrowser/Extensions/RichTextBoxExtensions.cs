using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CharacterBuilderBrowser
{
	public static class RichTextBoxExtensions
	{
		public static Inline GetFormattedText(DependencyObject obj)
		{
			return (Inline)obj.GetValue(FormattedTextProperty);
		}

		public static void SetFormattedText(DependencyObject obj,Inline value)
		{
			obj.SetValue(FormattedTextProperty,value);
		}

		public static readonly DependencyProperty FormattedTextProperty=
		    DependencyProperty.RegisterAttached(
			   "FormattedText",
			   typeof(Inline),
			   typeof(RichTextBoxExtensions),
			   new PropertyMetadata(null,OnFormattedTextChanged));

		private static void OnFormattedTextChanged(DependencyObject o,DependencyPropertyChangedEventArgs e)
		{
			var textBlock=o as RichTextBox;
			if(textBlock==null) return;

			textBlock.Document.Blocks.Clear();
			var inline=e.NewValue as Inline;
			if(inline!=null)
			{
				textBlock.Document.Blocks.Add(new Paragraph(inline));
			}
		}
	}
}
