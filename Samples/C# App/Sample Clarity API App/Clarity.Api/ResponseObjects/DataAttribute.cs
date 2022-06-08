using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class DataAttribute
	{
		public string name { get; set; }
		public string label { get; set; }
		public string type { get; set; }
		public string units { get; set; }
		public string report_group { get; set; }
		public int sort_order { get; set; }

		public override string ToString()
		{
			return label;
		}
	}
}
