using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorBattery
	{
		public Guid site_id { get; set; }
		public string site_name { get; set; }
		public Guid project_id { get; set; }
		public string project_name { get; set; }
		public string battery_name { get; set; }
		public string entity_name { get; set; }
		public string units { get; set; }
		public double current_reading { get; set; }
		public double full_reading { get; set; }
		public double low_reading { get; set; }
		public int? predicted_days_until_low { get; set; }

		public override string ToString()
		{
			return site_name + " - " + battery_name + ": " + current_reading + units;
		}
	}
}
