using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Clarity.ResponseObjects
{
	public class DataType
	{
		public string inspection_type { get; set; }
		public string data_type { get; set; }
		public string name { get; set; }
		public int record_count { get; set; }
		public string list_path { get; set; }

		string geo_json_layers;
		public string[] geoJsonLayers { get => JsonConvert.DeserializeObject<string[]>(geo_json_layers); }

		public override string ToString() 
		{
			return data_type;
		}
	}
}
