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

		public List<BaseCritter> GetCritters()
		{
			if (_critters == null)
			{
				_critters = new List<BaseCritter>();

				LoadCritterType("Bugs");
				LoadCritterType("Fish");
				LoadCritterType("Sea");
			}

			return _critters;
		}

		public List<BaseCritter> GetCritters(string critterType)
		{
			var critters = GetCritters();
			var filteredCritters = critters.Where(c => c.Type.Equals(critterType)).ToList();
			return filteredCritters;
		}

		public List<string> GetCritterTypes()
		{
			var critters = GetCritters();
			var types = critters.Select(c => c.Type).Distinct().ToList();
			return types;
		}

		private void LoadCritterType(string critterType)
		{
			Stream stream = System.Reflection.Assembly
				.GetExecutingAssembly().GetManifestResourceStream
				($"AnimalCrossing.Resources.Data.{critterType.ToLower()}.json");

			string json = new StreamReader(stream).ReadToEnd();

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
