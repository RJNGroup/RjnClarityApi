using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.RequestParameters
{
	public interface IRequestParameters
	{
		object[] ToParamArray(Api api, object Parent);
	}
}
