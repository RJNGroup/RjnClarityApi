using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class DataType
	{
		public string inspection_type { get; set; }
		public string data_type { get; set; }
		public string name { get; set; }
		public int record_count { get; set; }
		public string list_path { get; set; }
		public string[] geo_json_layers { get; set; }

		public override string ToString() 
		{
			return data_type;
		}
	}
}
