using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.ResponseObjects;
using System.ComponentModel;

namespace Clarity.RequestParameters
{
	public class DataTypesRequestParameters : IRequestParameters
	{

		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(true), Category("Options"), Description("This will include the counts in the result. Unfortunately it REALLY slows down the query, so only set this to true if you must.")]
		public bool include_counts { get; set; } = false;

		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is Project)
			{
				object[] r = { api, include_counts };
				result = r;
			}
			else
			{
				object[] r = { api, project, include_counts };
				result = r;
			}

			return result;
		}
	}
}
