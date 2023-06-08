using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Model
{
	public class SeaCritter : BaseCritter, IHasShadow, IHasSpeed
	{
		public override string Type { get { return "sea"; } }

		public string Shadow { get; set; }

		public string Speed { get; set; }
	}
}
