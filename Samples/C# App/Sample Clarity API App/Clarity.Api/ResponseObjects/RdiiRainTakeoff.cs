using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class RdiiRainTakeoff
	{
		public Guid site_id { get; set; }
		public Guid storm_id { get; set; }
		public string site_name { get; set; }
		public DateTime start_peak { get; set; }
		public int duration { get; set; }
		public double peak_rain { get; set; }
		public string duration_name { get; set; }
		public string recurrence_name { get; set; }

		public override string ToString() 
		{
			return site_name + ": " + duration_name + " = " + peak_rain.ToString() + "\"";
		}
	}
}
