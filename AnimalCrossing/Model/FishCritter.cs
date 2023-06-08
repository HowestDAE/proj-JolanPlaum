﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Model
{
	public class FishCritter : BaseCritter, IHasShadow
	{
		public override string Type { get { return "fish"; } }

		public string Shadow { get; set; }
	}
}
