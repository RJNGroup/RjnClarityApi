using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Utilities
{
	public class ClarityHttpCallEventArgs
	{
		public ClarityHttpCallEventArgs(string _path) 
		{
			path = _path;
		}

		public string path { get; }
	}

	public class ClarityHttpCallErrorEventArgs
	{
		public ClarityHttpCallErrorEventArgs(Exception _exeption, string _result)
		{
			e = _exeption;
			result = _result;
		}

		public Exception e { get; }
		public string result { get; }
	}
}
