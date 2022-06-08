using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public interface IDataListObject 
	{
		Guid id { get; set; }
		Guid parent_id { get; set; }
		Guid project_id { get; set; }
		string name { get; set; }
		DateTime created { get; set; }
		DateTime modified { get; set; }
		string info_path { get; set; }

	}
}
