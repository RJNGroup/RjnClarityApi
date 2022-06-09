using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.ResponseObjects;
using System.ComponentModel;

namespace Clarity.RequestParameters 

{
	public class RainTakeoffsByDurationTimeframeRequestParameters : IRequestParameters
	{
		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(true), Category("Options"), Description("The rainfall intesity duration in minutes.")]
		public int duration { get; set; }

		[Browsable(true), Category("Options"), Description("The number of days to look back and query storm events.")]
		public int timeframe { get; set; }

		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is Project)
			{
				object[] r = { api, duration, timeframe };
				result = r;
			}
			else
			{
				object[] r = { api, project, duration, timeframe };
				result = r;
			}

			return result;
		}
	}
}
