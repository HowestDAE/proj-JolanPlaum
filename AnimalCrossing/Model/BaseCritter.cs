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

		[JsonProperty("name")]
		private NestedName _name { get; set; }

		[JsonIgnore]
		public string Name { get { return _name.Name; } }

		[JsonProperty("price")]
		public int Price { get; set; }

		[JsonProperty("availability")]
		public Availability Available { get; set; }

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

	public class NestedName
	{
		[JsonProperty("name-EUen")]
		public string Name { get; set; }
	}

	public class Availability
	{
		[JsonProperty("month-array-northern")]
		public List<int> Months { get; set; }

		[JsonProperty("time-array")]
		public List<int> Times { get; set; }
	}
}
