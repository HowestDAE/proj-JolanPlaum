using AnimalCrossing.Model;
using AnimalCrossing.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
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

		private string _selectedType;
		public string SelectedType
		{
			get { return _selectedType; }
			set
			{
				_selectedType = value;
				OnPropertyChanged(nameof(SelectedType));

				SwitchTypesAsync();
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
			_localRepository = new CritterLocalRepository();

			//SetRepository(_localRepository);
		}

		public async Task SetRepositoryAsync(CritterLocalRepository repo)
		{
			_localRepository = repo;

			CritterList = await _localRepository.GetCrittersAsync();
			CritterTypes = await _localRepository.GetCritterTypesAsync();
			OnPropertyChanged(nameof(CritterList));
			OnPropertyChanged(nameof(CritterTypes));

			if (CritterTypes.Count > 0)
			{
				SelectedType = CritterTypes[0];
			}
		}

		private async Task SwitchTypesAsync()
		{
			CritterList = await _localRepository.GetCrittersAsync(SelectedType);
			OnPropertyChanged(nameof(CritterList));
		}
	}
}
