using AnimalCrossing.Model;
using AnimalCrossing.Repository;
using AnimalCrossing.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnimalCrossing.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		public string CommandText
		{
			get
			{
				if (CurrentPage is OverViewPage)
					return "SHOW DETAILS";
				else return "GO BACK";
			}
		}

		public string SwitchRepoCommandText
		{
			get
			{
				if (_critterRepository is CritterLocalRepository)
					return "GO ONLINE";
				else return "GO OFFLINE";
			}
		}

		private CritterLocalRepository _localRepository;
		private CritterApiRepository _apiRepository;
		private ICritterRepository _critterRepository;

		private OverViewPage MasterPage { get; } = new OverViewPage();

		private DetailPage CritterPage { get; } = new DetailPage();

		private Page _currentPage;

		public Page CurrentPage
		{
			get { return _currentPage; }
			set
			{
				_currentPage = value;
				OnPropertyChanged(nameof(CurrentPage));
				OnPropertyChanged(nameof(CommandText));
			}
		}

		public RelayCommand SwitchPageCommand { get; private set; }
		public RelayCommand SwitchRepositoryCommand { get; private set; }

		public MainViewModel()
		{
			_localRepository = new CritterLocalRepository();
			_apiRepository = new CritterApiRepository();
			_critterRepository = _apiRepository;

			CurrentPage = MasterPage;

			(MasterPage.DataContext as OverViewVM).SetRepositoryAsync(_critterRepository);

			SwitchPageCommand = new RelayCommand(SwitchPage);
			SwitchRepositoryCommand = new RelayCommand(SwitchRepository);
		}

		public void SwitchPage()
		{
			//check the current visible page type
			if (CurrentPage is OverViewPage) //overview page -> go to details page
			{
				//Get the selected pokemon
				BaseCritter selectedCritter = (MasterPage.DataContext as OverViewVM).SelectedCritter;
				if (selectedCritter == null) return;

				//Set the CurrentPokemon of the DetailPageVM to be the selected Pokemon
				(CritterPage.DataContext as DetailPageVM).Critter = selectedCritter;

				//Set the current page
				CurrentPage = CritterPage;
			}
			else
			{
				CurrentPage = MasterPage;
			}
		}

		public async void SwitchRepository()
		{
			if (_critterRepository is CritterLocalRepository)
			{
				_critterRepository = _apiRepository;
			}
			else
			{
				_critterRepository = _localRepository;
			}

			await (MasterPage.DataContext as OverViewVM).SetRepositoryAsync(_critterRepository);
			OnPropertyChanged(nameof(CurrentPage));
		}
	}
}
