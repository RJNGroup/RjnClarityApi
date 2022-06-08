using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Clarity.ResponseObjects
{
	public class DataRecordInfo
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string project { get; set; }
		public DateTime created { get; set; }
		public string created_by { get; set; }
		public dynamic attributes { get; set; }
		public MediaItem[] media { get; set; }
		public DataRecordInfo parent { get; set; }
		public Dictionary<string, DataRecordInfo[]> children { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	public class MediaItem
	{
		public Guid media_id { get; set; }
		public string name { get; set; }
		public string comments { get; set; }
		public string media_type { get; set; }
		public string url { get; set; }

		public override string ToString() 
		{
			return name;
		}
	}
}
