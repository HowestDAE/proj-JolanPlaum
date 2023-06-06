using AnimalCrossing.Model;
using AnimalCrossing.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.ViewModel
{
	public class OverViewVM : ObservableObject
	{
		private CritterLocalRepository _localRepository;
		public CritterLocalRepository LocalRepository
		{
			get { return _localRepository; }
			set
			{
				_localRepository = value;

				CritterList = _localRepository.GetCritters();
				CritterTypes = _localRepository.GetCritterTypes();

				if (CritterTypes.Count > 0)
				{
					SelectedType = CritterTypes[0];
				}
			}
		}

		private string _selectedType;
		public string SelectedType
		{
			get { return _selectedType; }
			set
			{
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
			set
			{
				_selectedCritter = value;
				OnPropertyChanged(nameof(SelectedCritter));
			}
		}

		public List<string> CritterTypes { get; private set; }
		public List<BaseCritter> CritterList { get; private set; }

		public OverViewVM()
		{
			LocalRepository = new CritterLocalRepository();
		}

	}
}
