using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Clarity.RequestParameters;
using System.Data;

namespace Clarity.ResponseObjects
{
	public class DataType
	{
		public string inspection_type { get; set; }
		public string data_type { get; set; }
		public string name { get; set; }
		public int? record_count { get; set; }
		public string list_path { get; set; }
		public Guid projectid { get; set; }
		public string geo_json_layers { get; set; }
		public string[] GetLayersArray { get => JsonConvert.DeserializeObject<string[]>(geo_json_layers); }

		public override string ToString() 
		{
			return data_type;
		}

		/// <summary>
		/// Gets the attribute list for a given data type.
		/// </summary>
		/// <returns>An array of DataAttribute objects.</returns>
		public DataAttribute[] GetDataAttributes(Api api) => api.GetDataAttributes(projectid, data_type);

		/// <summary>
		/// Gets the report group list for a given data type.
		/// </summary>
		/// <returns>An array of DataReportGroup objects.</returns>
		public DataReportGroup[] GetDataReportGroups(Api api) => api.GetDataReportGroups(projectid, data_type);

		/// <summary>
		/// Gets a record list matching the filters with any number of additional attributes.
		/// </summary>
		/// <param name="created_after">Optional filter by created date.</param>
		/// <param name="modified_after">Optional filter for modified date.</param>
		/// <param name="attributes">Optionally request additional attributes to return with the query.</param>
		/// <returns></returns>
		public DataTable GetDataRecordTable(Api api, DateTime? created_after = null, DateTime? modified_after = null, string[] attributes = null) => api.GetDataRecordTable(data_type, projectid, created_after, modified_after, attributes);

		/// <summary>
		/// For internal use.
		/// </summary>
		public DataRecordRequestParameters GetDataRecordTableRequestParameters = new DataRecordRequestParameters();

		/// <summary>
		/// Gets all filtered data in the GeoJSON format.
		/// </summary>
		/// <param name="created_after">Optional filter by created date.</param>
		/// <param name="modified_after">Optional filter for modified date.</param>
		/// <param name="attributes">Optionally request additional attributes to return with the query.</param>
		/// <returns>Returns a JSON string in the GeoJSON format.</returns>
		public string GetGeoJson(Api api, DateTime? created_after = null, DateTime? modified_after = null, string[] attributes = null) => api.GetGeoJson(data_type, projectid, created_after, modified_after, attributes);
		
		/// <summary>
		/// For internal use.
		/// </summary>
		public DataRecordRequestParameters GetGeoJsonRequestParameters = new DataRecordRequestParameters();

	}
}
