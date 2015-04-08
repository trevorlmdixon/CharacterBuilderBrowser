using System;
using System.Windows.Input;

namespace CharacterBuilderBrowser
{
	public class ShowRulesElementDetailsWindowCommand:ICommand
	{
		private IRulesElementRepository repository;

		public ShowRulesElementDetailsWindowCommand(IRulesElementRepository repository)
		{
			if(repository==null) throw new ArgumentNullException("repository");

			this.repository=repository;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			var element=parameter as RulesElement;
			if(element==null) return;

			var viewModel=new RulesElementDetailsViewModel(element,repository,this);
			var detailsWindow=new RulesElementDetailsWindow(viewModel);
			detailsWindow.Show();
		}
	}
}
