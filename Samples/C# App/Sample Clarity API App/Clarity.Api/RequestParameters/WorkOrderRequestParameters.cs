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
	public class WorkOrderRequestParameters : IRequestParameters
	{
		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(false)]
		public Guid? siteid { get; set; } = null;

		[Browsable(true), Category("Filters"), Description("OPTIONAL: List of statuses to query on.")]
		public string[] status_filter { get; set; } = { "Open" };

		[Browsable(true), Category("Filters"), Description("Get all work orders modified after this date.")]
		public DateTime? modified_after { get; set; } = null;

		private WorkOrderStatus[] GetStatusFilter()
		{
			return status_filter.Where(f =>
						{
							return Enum.TryParse(f.Trim(), true, out WorkOrderStatus _status);
						}).Select(f =>
						{
							return (WorkOrderStatus)Enum.Parse(typeof(WorkOrderStatus), f.Trim(), true);
						})
						.ToArray();
		}


		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && (Parent is MonitorSite || Parent is Project))
			{
				object[] r = { api, GetStatusFilter(), modified_after };
				result = r;
			}
			else
			{
				object[] r = { api, project, siteid, GetStatusFilter(), modified_after };
				result = r;
			}

			return result;
		}

	}
}
