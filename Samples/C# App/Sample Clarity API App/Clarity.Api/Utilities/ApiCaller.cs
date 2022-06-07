using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace Clarity.Utilities
{
	public class ApiCaller
	{
		const string base_uri = "https://rjn-clarity-api.com/v1/clarity";

		public static T GetResponseJson<T>(Authorizer auth, string path, Dictionary<string, string> query = null)
		{

			//Generate the URL

			//Create the web request
			var uri = CreateUri(path, query);
			var request = System.Net.HttpWebRequest.Create(uri);
			request.Method = "GET";

			//Add the token
			var token = auth.GetToken();
			request.Headers.Add("Authorization", $"Bearer {token}");

			//Raise the event
			auth.api.RaiseClarityHttpCallEvent("GET " + uri);

			//Call the api
			using (System.Net.WebResponse response = request.GetResponse())
			{
				var reader = new StreamReader(response.GetResponseStream());
				var responseJson = reader.ReadToEnd();
				try
				{
					return JsonConvert.DeserializeObject<T>(responseJson);
				}
				catch (Exception e)
				{
					//Raise an error
					auth.api.RaiseClarityHttpCallErrorEvent(e, responseJson);
					return default(T);
				}

			}
		}


		public static string PostData(Authorizer auth, string body, string path, Dictionary<string, string> query = null)
		{

			//Generate the URL

			//Create the web request
			var uri = CreateUri(path, query);
			var request = System.Net.HttpWebRequest.Create(uri);
			request.Method = "POST";

			//Add the token
			var token = auth.GetToken();
			request.Headers.Add("Authorization", $"Bearer {token}");
			request.ContentType = "application/json";

			//Create the payload
			byte[] payload = Encoding.UTF8.GetBytes(body);
			request.ContentLength = payload.Length;

			//Raise the event
			auth.api.RaiseClarityHttpCallEvent("POST " + uri);

			//Do the POST
			try
			{
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(payload, 0, payload.Length);

					using (System.Net.WebResponse response = request.GetResponse())
					{
						var reader = new StreamReader(response.GetResponseStream());
						var responseString = reader.ReadToEnd();
						return responseString;
					}
				}
			}
			catch (Exception e)
			{
				auth.api.RaiseClarityHttpCallErrorEvent(e, "");
				return null;
			}

		}

		static string CreateUri(string path, Dictionary<string, string> query)
		{
			//Add the query parameters if provided
			string q = null;
			if (query != null)
			{
				q = "";
				foreach (var param in query)
				{
					q += (q.Length > 0 ? "&" : "?") + param.Key + "=" + System.Web.HttpUtility.UrlEncode( param.Value);
				}
			}

			//Concatenate the path
			return base_uri + path + q;
		}
	}
}
