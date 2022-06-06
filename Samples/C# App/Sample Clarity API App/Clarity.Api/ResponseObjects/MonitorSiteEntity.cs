using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorSiteEntity
	{
		public string id { get; set; }
		public string name { get; set; }
		public string color { get; set; }
		public string unit { get; set; }
		public Guid site { get; set; }

		public override string ToString() 
		{
			return name + " (" + id + ")";
		}
	}
}
