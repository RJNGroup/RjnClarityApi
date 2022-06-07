using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class WorkOrder
	{
		public Guid id { get; set; }
		public long ref_id { get; set; }
		public Guid site_id { get; set; }
		public Guid project_id { get; set; }
		public string site_name { get; set; }
		public string project_name { get; set; }
		public DateTime created { get; set; }
		public DateTime modified { get; set; }
		public string status { get; set; }
		public string description { get; set; }
		public string urgency { get; set; }
		public DateTime? scheduled_date { get; set; }
		public string scheduled_for { get; set; }
		public string scheduled_for_contact { get; set; }
		public DateTime? resolution_date { get; set; }
		public string resolved_maintenance_url { get; set; }

		public override string ToString()
		{
			return site_name + ": " + description;
		}

	}
}
