using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.RequestParameters;
using System.Reflection;
using System.Dynamic;

namespace Sample_Clarity_API_App.UserInput
{
	public class UserInputUtil
	{
		public static object[] GetUserParameters(Api api, object Parent, IRequestParameters _params) {

			//Get user input
			var dialog = new ParametersDialog();
			dialog.GetUserInput(_params);

			return _params.ToParamArray(api, Parent);
		}


	}
}
