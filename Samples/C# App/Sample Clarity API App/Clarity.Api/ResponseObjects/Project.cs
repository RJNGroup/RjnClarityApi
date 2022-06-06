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


		public override string ToString()
		{
			return projectnumber + ": " + description;
		}
	}
}
