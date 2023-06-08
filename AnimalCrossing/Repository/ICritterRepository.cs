using AnimalCrossing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Repository
{
	public interface ICritterRepository
	{
		Task<List<BaseCritter>> GetCrittersAsync();

		Task<List<BaseCritter>> GetCrittersAsync(string critterType);

		Task<List<string>> GetCritterTypesAsync();
	}
}
