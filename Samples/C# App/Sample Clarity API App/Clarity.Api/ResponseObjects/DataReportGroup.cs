using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class DataReportGroup
	{
		public int id { get; set; }
		public string name { get; set; }
		public int sort_order { get; set; }

		public override string ToString()
		{
			return name;
		}
	}
}
