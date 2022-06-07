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
	public class MonitorAlarmRequestParameters : IRequestParameters
	{

		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(true), Category("Timeframe"), Description("The timeframe, in hours, to look back from the present to find alarm events.")]
		public int timeframe { get; set; } = 48;



		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is Project)
			{
				object[] r = { api, timeframe };
				result = r;
			}
			else
			{
				object[] r = { api, project, timeframe };
				result = r;
			}

			return result;
		}
	}
}
