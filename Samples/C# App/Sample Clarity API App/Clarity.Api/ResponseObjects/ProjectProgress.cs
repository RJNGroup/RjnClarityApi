using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class ProjectProgress
	{
		public Guid projectid { get; set; }
		public string projectname { get; set; }
		public string inspection_type { get; set; }
		public DateTime? last_record { get; set; }
		public int completed_units { get; set; }
		public int targeted_units { get; set; }
		public string unit { get; set; }
		public double percent_complete { get; set; }
		public bool work_started { get; set; }
		public bool work_complete { get; set; }

		public override string ToString()
		{
			return projectname + ": " + inspection_type + " --- " + percent_complete + "%";
		}
	}
}
