using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Enums
{
	public enum AggregationInterval
	{
		Native = 0,
		Five_Minute = 5,
		Ten_Minute = 10,
		Fifteen_Minute = 15,
		Thirty_Minute = 30,
		Hourly = 60,
		Four_Hour = 240,
		Six_Hour = 360,
		Twelve_Hour = 720,
		Daily = 1440,
		Weekly = 10080,
		Monthly = 44700,
		Quarterly = 133920,
		Annualy = 527100
	}
}
