using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorUnit
	{
		public int id { get; set; }
		public string name { get; set; }
		public string long_name { get; set; }
		public string type { get; set; }
	
		public override string ToString()
		{
			return name;
		}
	}
}
