using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Clarity.RequestParameters
{
	class DataImportBody
	{
		public string comments;
		public Dictionary<DateTime, double?> data;

		public DataImportBody(Dictionary<DateTime, double?>  _data, string _comments)
		{
			comments = _comments == null ? "" : _comments;
			data = _data;
		}

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
