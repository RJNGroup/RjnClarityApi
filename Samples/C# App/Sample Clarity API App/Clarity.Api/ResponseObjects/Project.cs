using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class Project
	{
		public Guid id { get; set; }
		public string description { get; set; }
		public string projectnumber { get; set; }

		/// <summary>
		/// Gets the datatypes for this project.
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public DataType[] GetDataTypes(Api api) => api.GetDataTypes(id);

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

		public override string ToString()
		{
			return projectnumber + ": " + description;
		}
	}
}
