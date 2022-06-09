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
	public class DataRecordRequestParameters : IRequestParameters
	{
		[Browsable(false)]
		public string record_type { get; set; }

		[Browsable(false)]
		public Guid project { get; set; }

		[Browsable(true), Category("Filters"), Description("OPTIONAL: Filter records created after the specified date.")]
		public DateTime? created_after { get; set; }

		[Browsable(true), Category("Filters"), Description("OPTIONAL: Filter records modified after the specified date.")]
		public DateTime? modified_after { get; set; }

		[Browsable(true), Category("Data"), Description("OPTIONAL: Additional attributes to include in the query (use attribute names, no labels).")]
		public string[] attributes { get; set; }


		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is RecordType)
			{
				object[] r = { api, created_after, modified_after, attributes };
				result = r;
			}
			else
			{
				object[] r = { api, record_type, project, created_after, modified_after, attributes };
				result = r;
			}

			return result;
		}

	}
}
