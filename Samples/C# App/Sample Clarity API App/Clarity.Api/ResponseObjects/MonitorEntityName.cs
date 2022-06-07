using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorEntityName
	{
		public string name { get; set; }
		public int count { get; set; }
		public string entities_path { get; set; }

		public override string ToString()
		{
			return name;
		}
	}
}
