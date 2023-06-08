using AnimalCrossing.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.ViewModel
{
	public class DetailPageVM : ObservableObject
	{
		private BaseCritter _critter = new SeaCritter()
		{
			Id = 1,
			Available = new Availability(){ Months = "3-9",
											Times = "5-17"},
			Price = 32000
		};

		public BaseCritter Critter
		{
			get { return _critter; }
			set
			{
				_critter = value;

				_infoList = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("Months", _critter.Available.Months),
					new KeyValuePair<string, string>("Times", _critter.Available.Times),
					new KeyValuePair<string, string>("Price", _critter.Price.ToString())
				};

				if (_critter is IHasShadow shadow)
				{
					_infoList.Add(new KeyValuePair<string, string>("Shadow", shadow.Shadow));
				}
				if (_critter is IHasSpeed speed)
				{
					_infoList.Add(new KeyValuePair<string, string>("Speed", speed.Speed));
				}
			}
		}

		private List<KeyValuePair<string,string>> _infoList;

		public List<KeyValuePair<string, string>> InfoList
		{
			get { return _infoList; }
			set { _infoList = value; }
		}

	}
}
