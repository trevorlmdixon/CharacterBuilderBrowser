﻿using System.Windows;

namespace DnD.CharacterBuilder.Browser
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
