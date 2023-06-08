using AnimalCrossing.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Repository
{
	public class CritterApiRepository : ICritterRepository
	{
		private readonly string _baseUrl = "http://acnhapi.com/v1a/";
		private List<BaseCritter> _critters;

		public async Task<List<BaseCritter>> GetCrittersAsync()
		{
			if (_critters == null)
			{
				_critters = new List<BaseCritter>();

				await LoadCritterTypeAsync("Bugs");
				await LoadCritterTypeAsync("Fish");
				await LoadCritterTypeAsync("Sea");
			}

			return _critters;
		}

		public async Task<List<BaseCritter>> GetCrittersAsync(string critterType)
		{
			var critters = await GetCrittersAsync();
			var filteredCritters = critters.Where(c => c.Type.Equals(critterType)).ToList();
			return filteredCritters;
		}

		public async Task<List<string>> GetCritterTypesAsync()
		{
			var critters = await GetCrittersAsync();
			var types = critters.Select(c => c.Type).Distinct().ToList();
			return types;
		}

		private async Task LoadCritterTypeAsync(string critterType)
		{
			string endpoint = $"{_baseUrl}{critterType.ToLower()}/";

			using (HttpClient client = new HttpClient())
			{
				try
				{
					var response = await client.GetAsync(endpoint);

					if (!response.IsSuccessStatusCode)
						throw new HttpRequestException(response.ReasonPhrase);

					string json = await response.Content.ReadAsStringAsync();

					Type type = Type.GetType($"AnimalCrossing.Model.{critterType}Critter");
					var data = JArray.Parse(json);

					foreach (JToken token in data)
					{
						BaseCritter critter = (BaseCritter)token.ToObject(type);
						if (critter.Available.Months == "") critter.Available.Months = "All year";
						if (critter.Available.Times == "") critter.Available.Times = "All day";
						_critters.Add(critter);
					}
				}
				catch (Exception) { }
			}
		}
	}
}
