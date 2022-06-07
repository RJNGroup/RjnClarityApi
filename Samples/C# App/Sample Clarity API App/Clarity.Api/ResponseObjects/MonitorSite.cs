using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.RequestParameters;
using Clarity.Enums;

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
		public MonitorEntity[] GetMonitorSiteEntities(Api api) => api.GetMonitorEntities(null, id);


		/// <summary>
		/// Gets all work orders that match the filters provided.
		/// </summary>
		/// <param name="status_filter">OPTIONAL: A list of statuses to filter for. By default, the query will return all open work orders.</param>
		/// <param name="modified_after">OPTIONAL: Gets work orders modified after a specific date.</param>
		/// <returns></returns>
		public WorkOrder[] GetWorkOrders(Api api, WorkOrderStatus[] status_filter = null, DateTime? modified_after = null) => api.GetWorkOrders(api.GetProjectFromSiteId(id), id, status_filter, modified_after);

		public WorkOrderRequestParameters GetWorkOrdersRequestParameters = new WorkOrderRequestParameters();

	}
}
