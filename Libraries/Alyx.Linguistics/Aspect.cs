using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public enum Aspect
	{
		Unspecified = 0,
		Simple,
		/// <summary>
		/// The continuous (or present) gerund aspect; for example, "loving" or "swimming".
		/// </summary>
		Continuous,
		/// <summary>
		/// The perfect gerund aspect; for example, "having loved" or "having swum".
		/// </summary>
		Perfect,
		PerfectContinuous
	}
}
