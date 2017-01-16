using System;
using System.Windows.Input;

namespace DnD.CharacterBuilder.Browser
{
	public class RulesElementDetailsViewModel
	{
		public RulesElementDetailsViewModel(RulesElement element,IRulesElementRepository repository,ICommand viewElementCommand, ICommand exportCommand)
		{
			if(element==null) throw new ArgumentNullException("element");
			if(repository==null) throw new ArgumentNullException("repository");
			if(viewElementCommand==null) throw new ArgumentNullException("viewElementCommand");
            if(exportCommand == null) throw new ArgumentNullException("exportCommand");

			this.Element=element;
			this.Repository=repository;
			this.ViewElementCommand=viewElementCommand;
            this.ExportCommand=exportCommand;
		}

		public RulesElement Element { get; private set; }
		public IRulesElementRepository Repository { get; private set; }
		public ICommand ViewElementCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
	}
}
