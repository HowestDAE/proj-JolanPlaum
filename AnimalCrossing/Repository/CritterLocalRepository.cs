using AnimalCrossing.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Repository
{
	public class CritterLocalRepository
	{
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
			Stream stream = System.Reflection.Assembly
				.GetExecutingAssembly().GetManifestResourceStream
				($"AnimalCrossing.Resources.Data.{critterType.ToLower()}.json");

			string json = await new StreamReader(stream).ReadToEndAsync();

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
	}
}
