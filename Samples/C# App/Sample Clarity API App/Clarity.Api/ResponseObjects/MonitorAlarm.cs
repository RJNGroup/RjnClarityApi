using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Clarity.ResponseObjects
{
	public class MonitorAlarm
	{
		public Guid alarm_id { get; set; }
		public string alarm_name { get; set; }
		public string alarm_level { get; set; }
		public Guid site_id { get; set; }
		public string site_name { get; set; }
		public bool activated { get; set; }
		public bool enabled { get; set; }
		public bool muted { get; set; }
		public DateTime? last_state_change { get; set; }
		public DateTime? last_evaluation { get; set; }
		public MonitorAlarmEvent[] events { get; set; }

		public string events_json
		{
			get {
				return JsonConvert.SerializeObject(events);
			}
		}

		public override string ToString() 
		{
			return site_name + " - " + alarm_name;
		}
}

	public class MonitorAlarmEvent
	{
		public DateTime start { get; set; }
		public DateTime? end { get; set; }
	}
}
