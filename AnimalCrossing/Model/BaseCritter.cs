using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Model
{
	public abstract class BaseCritter
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name.name-EUen")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public int Price { get; set; }

		[JsonProperty("availability.month-array-northern")]
		public List<int> Months { get; set; }

		[JsonProperty("availability.time-array")]
		public List<int> Times { get; set; }

		[JsonIgnore]
		public abstract string Type { get; }

		[JsonProperty]
		public string UrlEndpoint
		{
			get
			{
				return $"/{Type}/{Id}";
			}
		}
	}
}
