using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class SsesDefect
	{
		public Guid inspection_id { get; set; }
		public string inspection_recordtype { get; set; }
		public Guid project_id { get; set; }
		public string defect_name { get; set; }
		public string category { get; set; }
		public string sector { get; set; }
		public string coordinates { get; set; }
		public string inspection_info { get; set; }
		public double ii_gpd { get; set; }
		public string identified_in { get; set; }
		public DateTime modified { get; set; }

		public override string ToString()
		{
			return defect_name + ": " + inspection_info;
		}

	}
}
