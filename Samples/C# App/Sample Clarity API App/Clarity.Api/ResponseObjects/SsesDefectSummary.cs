using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class SsesDefectSummary
	{
		public string defect_name { get; set; }
		public string category { get; set; }
		public string identified_in { get; set; }
		public double total_ii_gpm { get; set; }
		public int total_count { get; set; }

		public override string ToString()
		{
			return defect_name;
		}
	}
}
