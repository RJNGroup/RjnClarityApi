using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Enums;
using Clarity.RequestParameters;

namespace Clarity.ResponseObjects
{
	public class MonitorEntity
	{
		public string id { get; set; }
		public string name { get; set; }
		public string color { get; set; }
		public string unit { get; set; }
		public Guid site { get; set; }


		/// <summary>
		/// Gets time series data from the specified flow monitor or rain gauge entity.
		/// </summary>
		/// <param name="from">The lower bound date and time.</param>
		/// <param name="to">The upper bould date and time (if left null, the current time is assumed)</param>
		/// <param name="unitid">The unit to convert to (if left null, it will return values in the default units)</param>
		/// <param name="interval">The aggregation interval (if left null, it will return the native interval, or the interval as recorded by the meter).</param>
		/// <param name="aggregation">The type of aggregation, if the interval is non-native, to apply to the compressed interval.</param>
		/// <returns>A dictionary of date / value pairs.</returns>
		public MonitorDataPoint[] GetMonitorData(Api api, DateTime from, DateTime? to = null, int unitid = -1, AggregationInterval interval = AggregationInterval.Native, AggregationType aggregation = AggregationType.Average)
		{
			var project = api.GetProjectFromSiteId(site);
			return api.GetMonitorData(project, id, from, to, unitid, interval, aggregation);
		}

		public MonitorDataRequestParameters GetMonitorDataRequestParameters = new MonitorDataRequestParameters();

		public override string ToString() 
		{
			return name + " (" + id + ")";
		}
	}
}
