using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ResponseObjects
{
	public class MonitorDataPoint
	{
		public DateTime time { get; set; }
		public double value { get; set; }

		public MonitorDataPoint(DateTime _time, double _value)
		{
			time = _time;
			value = _value;
		}

		public override string ToString()
		{
			return time.ToString("yyyy-MM-dd HH:mm") + ", " + value.ToString();
		}
	}
}
