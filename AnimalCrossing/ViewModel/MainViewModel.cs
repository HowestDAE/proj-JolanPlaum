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
		private CritterLocalRepository _localRepository;

		private OverViewPage MasterPage { get; } = new OverViewPage();

		private Page _currentPage;

		public Page CurrentPage
		{
			get { return _currentPage; }
			set
			{
				_currentPage = value;
				OnPropertyChanged(nameof(CurrentPage));
			}
		}

		public MainViewModel()
		{
			_localRepository = new CritterLocalRepository();

			CurrentPage = MasterPage;

			(MasterPage.DataContext as OverViewVM).LocalRepository = _localRepository;
		}
	}
}
