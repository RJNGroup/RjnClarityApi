using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Clarity.Utilities
{
	public class Authorizer
	{
		private string _token = null;
		private DateTime _expiration;

		private string _id;
		private string _pw;

		const string auth_url = "https://rjn-clarity-api.com/v1/clarity/auth";

		public Authorizer(string client_id, string password)
		{
			_id = client_id;
			_pw = password;
		}
	
		public bool Validate() 
		{
			return GetToken() != null ? true : false;
			
		}
	
		public string GetToken()
		{
			//Use the cached token if valid
			if (_token != null)
			{
				//Check the expiration (if it's within ten minutes, time to get a new one).
				if (_expiration.AddMinutes(-10) > DateTime.Now)
				{
					return _token;
				}
			}

			//Create the web request
			var request = System.Net.HttpWebRequest.Create(auth_url);
			request.Method = "POST";
			request.ContentType = "application/json";

			//Create the payload
			string credentialsJson = JsonConvert.SerializeObject(new { client_id = _id, password = _pw });
			byte[] payload = Encoding.UTF8.GetBytes(credentialsJson);
			request.ContentLength = payload.Length;

			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(payload, 0, payload.Length);

				using (System.Net.WebResponse response = request.GetResponse())
				{
					var reader = new StreamReader(response.GetResponseStream());
					var responseJson = reader.ReadToEnd();
					var responseObj = JsonConvert.DeserializeObject<TokenResponse>(responseJson);

					_token = responseObj.token;
					_expiration = responseObj.GetExpirationDate();
					return _token;
				}
			}
			
		}

		class TokenResponse
		{
			public string token;
			public long expires;

			public DateTime GetExpirationDate()
			{
				return DateTime.Parse("1970-01-01").AddMilliseconds(expires);
			}
		}

	}
}
