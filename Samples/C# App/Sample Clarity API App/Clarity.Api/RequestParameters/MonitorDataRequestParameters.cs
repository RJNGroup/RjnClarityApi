using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Enums;
using Clarity.ResponseObjects;
using System.ComponentModel;

namespace Clarity.RequestParameters
{
	public class MonitorDataRequestParameters : IRequestParameters
	{
		
		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(false)]
		public string entityid { get; set; }

		[Browsable(true), Category("Date Range"), DisplayName("from*"), Description("REQUIRED: The lower bound date and time to query the data.")]
		public DateTime from { get; set; } = DateTime.Now.AddDays(-7);

		[Browsable(true), Category("Date Range"), Description("The upper bound date and time to query the data. If left blank, the upper bound is assumed to be now.")]
		public DateTime? to { get; set; } = null;

		[Browsable(true), Category("Units"), Description("The unit id to convert to. If less than zero, the data will be returned in the default units.")]
		public int unitid { get; set; } = -1;

		[Browsable(true), Category("Aggregation"), Description("The aggregation interval.")]
		public AggregationInterval interval { get; set; } = AggregationInterval.Native;

		[Browsable(true), Category("Aggregation"), Description("The aggregation method.")]
		public AggregationType aggregation { get; set; } = AggregationType.Average;

		public object[] ToParamArray(Api api, object Parent) 
		{
			object[] result;
			if (Parent != null && Parent is MonitorEntity)
			{
				object[] r = { api, from, to, unitid, interval, aggregation };
				result = r;
			} 
			else 
			{
				object[] r = { api, project, entityid, from, to, unitid, interval, aggregation };
				result = r;
			}
			
			return result;
		}


	}
}
