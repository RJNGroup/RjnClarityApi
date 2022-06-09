using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.RequestParameters;
using Clarity.Enums;

namespace Clarity.ResponseObjects
{
	public class Project
	{
		public Guid id { get; set; }
		public string description { get; set; }
		public string projectnumber { get; set; }

		/// <summary>
		/// Gets the recordtypes for this project.
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public RecordType[] GetRecordTypes(Api api, bool include_counts = false) => api.GetRecordTypes(id, include_counts);

		public RecordTypesRequestParameters GetRecordTypesRequestParameters = new RecordTypesRequestParameters();

		/// <summary>
		/// Gets an array of flow meter sites.
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public MonitorSite[] GetMonitorSites(Api api) => api.GetMonitorSites(id);


		/// <summary>
		/// Gets an array of flow meter site entity names for the project (raw sensor data or calculated measurement entities).
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public MonitorEntityName[] GetMonitorEntityNames(Api api) => api.GetMonitorEntityNames(id);


		/// <summary>
		/// Gets an array of units configured on the project.
		/// </summary>
		/// <param name="project"></param>
		/// <returns>An array of MonitorEntityName objects</returns>
		public MonitorUnit[] GetMonitorUnits(Api api) => api.GetMonitorUnits(id);

		/// <summary>
		/// Gets all active alarms.
		/// </summary>
		/// <param name="project">The project guid.</param>
		/// <returns>An array of MonitorAlarm objects</returns>
		public MonitorAlarm[] GetActiveAlarms(Api api) => api.GetActiveAlarms(id);

		/// <summary>
		/// Gets all active alarms.
		/// </summary>
		/// <param name="project">The project guid.</param>
		/// <param name="timeframe">The timeframe to look back in hours.</param>
		/// <returns>An array of MonitorAlarm objects</returns>
		public MonitorAlarm[] GetAlarms(Api api, int timeframe) => api.GetAlarms(id, timeframe);

		/// <summary>
		/// For internal use.
		/// </summary>
		public MonitorAlarmRequestParameters GetAlarmsRequestParameters = new MonitorAlarmRequestParameters();

		/// <summary>
		/// Gets all work orders that match the filters provided.
		/// </summary>
		/// <param name="project">The project guid.</param>
		/// <param name="status_filter">OPTIONAL: A list of statuses to filter for. By default, the query will return all open work orders.</param>
		/// <param name="modified_after">OPTIONAL: Gets work orders modified after a specific date.</param>
		/// <returns></returns>
		public WorkOrder[] GetWorkOrders(Api api, WorkOrderStatus[] status_filter = null, DateTime? modified_after = null) => api.GetWorkOrders(id, null, status_filter, modified_after);

		/// <summary>
		/// For internal use.
		/// </summary>
		public WorkOrderRequestParameters GetWorkOrdersRequestParameters = new WorkOrderRequestParameters();

		public override string ToString()
		{
			return projectnumber + ": " + description;
		}
	}
}
