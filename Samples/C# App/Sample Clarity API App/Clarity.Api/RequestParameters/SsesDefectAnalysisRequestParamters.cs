using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.ResponseObjects;
using System.ComponentModel;

namespace Clarity.RequestParameters
{
	public class SsesDefectAnalysisRequestParamters : IRequestParameters
	{

		[Browsable(false)]
		public Guid project { get; set; }


		[Browsable(true), Category("Filters"), Description("OPTIONAL: Filter for certain inspection types.")]
		public string[] inspection_types { get; set; } = null;

		[Browsable(true), Category("Filters"), Description("OPTIONAL: Filter for certain defect types.")]
		public string[] defect_types { get; set; } = null;

		[Browsable(true), Category("Filters"), Description("OPTIONAL: Filter for defects modified after this date.")]
		public DateTime? modified_after { get; set; } = null;


		public object[] ToParamArray(Api api, object Parent)
		{
			object[] result;
			if (Parent != null && Parent is Project)
			{
				object[] r = { api, inspection_types, defect_types, modified_after };
				result = r;
			}
			else
			{
				object[] r = { api, project, inspection_types, defect_types, modified_after };
				result = r;
			}

			return result;
		}

	}
}
