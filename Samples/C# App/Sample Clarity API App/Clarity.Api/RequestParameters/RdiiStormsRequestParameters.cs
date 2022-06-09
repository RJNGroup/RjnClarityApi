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
	public class RdiiStormsRequestParameters : IRequestParameters
	{
		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(true), Category("Date Range"), Description("Filter for storm events after this date.")]
		public DateTime? occur_after { get; set; }

		[Browsable(true), Category("Date Range"), Description("Filter for storm events before this date.")]
		public DateTime? occur_before { get; set; }

		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is Project)
			{
				object[] r = { api, occur_after, occur_before };
				result = r;
			}
			else
			{
				object[] r = { api, project, occur_after, occur_before };
				result = r;
			}

			return result;
		}


	}
}
