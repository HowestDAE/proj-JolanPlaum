using AnimalCrossing.Model;
using AnimalCrossing.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		private CritterLocalRepository _localRepository;

		private string _selectedType;
		public string SelectedType
		{
			get { return _selectedType; }
			set {
				_selectedType = value;
				OnPropertyChanged(nameof(SelectedType));

				CritterList = _localRepository.GetCritters(SelectedType);
				OnPropertyChanged(nameof(CritterList));
			}
		}

		private BaseCritter _selectedCritter;
		public BaseCritter SelectedCritter
		{
			get { return _selectedCritter; }
			set {
				_selectedCritter = value;
				OnPropertyChanged(nameof(SelectedCritter));
			}
		}

		public List<string> CritterTypes { get; private set; }
		public List<BaseCritter> CritterList { get; private set; }

		public MainViewModel()
		{
			_localRepository = new CritterLocalRepository();

			CritterList = _localRepository.GetCritters();
			CritterTypes = _localRepository.GetCritterTypes();

			if (CritterTypes.Count > 0)
			{
				SelectedType = CritterTypes[0];
			}
		}
	}
}
