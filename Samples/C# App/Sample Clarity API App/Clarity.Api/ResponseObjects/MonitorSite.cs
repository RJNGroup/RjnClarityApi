using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorSite
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string monitor_type { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }
		public string asset_or_facility { get; set; }
		public string site_info_path { get; set; }

		public override string ToString() {
			return name;
		}

		/// <summary>
		/// Gets an array of flow meter site entities (raw sensor data or calculated measurement entities).
		/// </summary>
		/// <param name="api"></param>
		/// <returns>An array of MonitorSiteEntity objects</returns>
		public MonitorSiteEntity[] GetMonitorSiteEntities(Api api) => api.GetMonitorEntities(null, id);

	}
}
