using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class RdiiStorm
	{
		public Guid storm_id { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public string name { get; set; }
		public int duration_hours { get; set; }
		public int max_recurrence_months { get; set; }
		public string max_recurrence { get; set; }
		public bool in_progress { get; set; }
		public double max_rain_total { get; set; }

		/// <summary>
		/// Gets the rain takeoffs for the storm event.
		/// </summary>
		/// <param name="api"></param>
		/// <returns>Returns an array of RdiiRainTakeoff objects.</returns>
		public RdiiRainTakeoff[] GetRainTakeoffs(Api api) => api.GetRainTakeoffs(storm_id); 

		public override string ToString()
		{
			return name;
		}
	}
}
