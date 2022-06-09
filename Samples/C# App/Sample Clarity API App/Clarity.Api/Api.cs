using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Utilities;
using Clarity.ResponseObjects;
using Clarity.RequestParameters;
using Clarity.Enums;
using Newtonsoft.Json;
using System.Data;

namespace Clarity
{
    public class Api
    {


        private Authorizer _auth;
        public Authorizer Authorizer { get { return _auth; } }

        public Api(string client_id, string password)
        {
            _auth = new Authorizer(this, client_id, password);
        }

        public bool Validate()
        {
            return _auth.Validate();
        }

        #region Events
        public delegate void ClarityHttpCallHandler(object sender, ClarityHttpCallEventArgs e);
        public delegate void ClarityHttpCallErrorHandler(object sender, ClarityHttpCallErrorEventArgs e);

        public event ClarityHttpCallHandler ClarityHttpCall;
        public event ClarityHttpCallErrorHandler ClarityHttpError;

        public void RaiseClarityHttpCallEvent(string path)
        {
            ClarityHttpCall?.Invoke(this, new ClarityHttpCallEventArgs(path));
        }

        public void RaiseClarityHttpCallErrorEvent(Exception e, string result)
        {
            ClarityHttpError?.Invoke(this, new ClarityHttpCallErrorEventArgs(e, result));
        }
        #endregion

        #region General

        private string FormatQueryDate(DateTime d)
        {
            return d.ToString("yyyy-MM-ddTHH:mm:00.000");
		}

        /// <summary>
        /// Get the all projects authorized for the user.
        /// </summary>
        /// <returns>An array of projects.</returns>
        public Project[] GetProjects()
        {
            return ApiCaller.GetResponseJson<Project[]>(_auth, "/projects");
        }

        /// <summary>
        /// Get all project status including percent complete for each work type.
        /// </summary>
        /// <returns>An array of project status objects.</returns>
        public ProjectProgress[] GetProjectProgress()
        {
            return ApiCaller.GetResponseJson<ProjectProgress[]>(_auth, "/projects/progress");
        }

        /// <summary>
        /// Gets the data types that can be queried such as Struct Inspection, Smoke Testing, or Flow Metering Mainteannce.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="include_counts">Setting this to true REALLY slows down the query, so only do it if you must.</param>
        /// <returns>An array of RecordType objects</returns>
        public RecordType[] GetRecordTypes(Guid project, bool include_counts = false)
        {
            var query = new Dictionary<string, string>();
            query.Add("include_counts", include_counts.ToString().ToLower());
            var items = ApiCaller.GetResponseJson<RecordType[]>(_auth, $"/projects/{project}/recordtypes", query);
            foreach (var item in items)
            {
                item.projectid = project;
			}
            return items;
        }

        /// <summary>
        /// Gets the pre-signed URL of the media file hosted on AWS.
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string GetMediaURL(Guid media_id)
        {
            return ApiCaller.GetResponseJson<string>(_auth, $"/media/{media_id}");
		}

        #endregion

        #region Flow Monitoring
        /// <summary>
        /// Gets an array of flow meter sites.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorSite objects</returns>
        public MonitorSite[] GetMonitorSites(Guid project)
        {
            var results = ApiCaller.GetResponseJson<MonitorSite[]>(_auth, $"/projects/{project}/sites");

            //Cache the sites list for later reference
            if (!_SiteProjectMap.ContainsKey(project))
            {
                _SiteProjectMap.Add(project, new List<MonitorSite>());
            }
            _SiteProjectMap[project] = results.ToList();

            return results;
        }

        /// <summary>
        /// If the site list has already been called, this function will return the project id associated with the site from the cache.
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public Guid GetProjectFromSiteId(Guid site)
        {
            return _SiteProjectMap.SingleOrDefault(k => k.Value.SingleOrDefault(s => s.id == site) != null).Key;
        }

        private Dictionary<Guid, List<MonitorSite>> _SiteProjectMap = new Dictionary<Guid, List<MonitorSite>>();

        /// <summary>
        /// Gets an array of flow meter site entities (raw sensor data or calculated measurement entities).
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorEntity objects</returns>
        public MonitorEntity[] GetMonitorEntities(Guid? project, Guid site)
        {
            Guid _project;
            if (project.HasValue) {
                _project = project.Value;
            }
            else 
            {
                _project = GetProjectFromSiteId(site);
            }
            return ApiCaller.GetResponseJson<MonitorEntity[]>(_auth, $"/projects/{_project}/sites/{site}/entities");
        }

        /// <summary>
        /// Gets an array of flow meter site entities (raw sensor data or calculated measurement entities).
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entity_name"></param>
        /// <returns>An array of MonitorEntity objects</returns>
        public MonitorEntity[] GetMonitorEntities(Guid project, string entity_name)
        {
            return ApiCaller.GetResponseJson<MonitorEntity[]>(_auth, $"/projects/{project}/entities/{entity_name}");
        }

        /// <summary>
        /// Gets an array of flow meter site entity names for the project (raw sensor data or calculated measurement entities).
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorEntityName objects</returns>
        public MonitorEntityName[] GetMonitorEntityNames(Guid project)
        {
            return ApiCaller.GetResponseJson<MonitorEntityName[]>(_auth, $"/projects/{project}/entities");
        }

        /// <summary>
        /// Gets an array of units configured on the project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorEntityName objects</returns>
        public MonitorUnit[] GetMonitorUnits(Guid project)
        {
            return ApiCaller.GetResponseJson<MonitorUnit[]>(_auth, $"/projects/{project}/units");
        }

        /// <summary>
        /// Gets time series data from the specified flow monitor or rain gauge entity.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entityid">The id of the entity (an integer id that is usually prefixed with 's' or 'm' but sometimes has no prefix).</param>
        /// <param name="from">The lower bound date and time.</param>
        /// <param name="to">The upper bould date and time (if left null, the current time is assumed)</param>
        /// <param name="unitid">The unit to convert to (if left null, it will return values in the default units)</param>
        /// <param name="interval">The aggregation interval (if left null, it will return the native interval, or the interval as recorded by the meter).</param>
        /// <param name="aggregation">The type of aggregation, if the interval is non-native, to apply to the compressed interval.</param>
        /// <returns>A dictionary of date / value pairs.</returns>
        public MonitorDataPoint[] GetMonitorData(Guid project, string entityid, DateTime from, DateTime? to = null, int unitid = -1, AggregationInterval interval = AggregationInterval.Native, AggregationType aggregation = AggregationType.Average)
        {

            //Create the query parameter list
            var query = new Dictionary<string, string>();
            query.Add("from", FormatQueryDate(from));
            if (to.HasValue) query.Add("to", FormatQueryDate(to.Value));
            if (unitid > 0) query.Add("unit", unitid.ToString());
            query.Add("interval", ((int)interval).ToString());
            query.Add("aggregation", aggregation.ToString());

            //Get the result
            var result = ApiCaller.GetResponseJson<Dictionary<DateTime, double>>(_auth, $"/projects/{project}/entities/{entityid}/data", query);

            //Transform the result
            return result.Select(k => new MonitorDataPoint(k.Key, k.Value)).ToArray();
        }

        /// <summary>
        /// Pushes flow monitor data into Clarity.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="entityid">The destination entity identifier.</param>
        /// <param name="data">A dictionary of time and value key/value pairs.</param>
        /// <param name="interval">The recording interval in seconds. If variable interval is utilized, set this as the "slow" mode interval.</param>
        /// <param name="import_mode">Specify how to handle existing data within the import window.</param>
        /// <param name="incoming_time">Specify the type of timezone of the data being imported.</param>
        /// <param name="local_timezone">OPTIONAL: Only set this if the incoming timezone is UTC.</param>
        /// <returns>A status message.</returns>
        public string PushMonitorData(Guid project, string entityid, Dictionary<DateTime, double?> data, int interval, ImportMode import_mode, ImportTimeType incoming_time, LocalTimezoneOption local_timezone = LocalTimezoneOption.Unset, string comments = null) 
        {
            //Add the query parameters
            var query = new Dictionary<string, string>();
            query.Add("interval", interval.ToString());
            query.Add("import_mode", import_mode.ToString());
            query.Add("incoming_time", incoming_time.ToString());
            query.Add("local_timezone", local_timezone.ToString());

            //Create the body
            var body = JsonConvert.SerializeObject(new DataImportBody(data, comments));

            //Post the data
            return ApiCaller.PostData(_auth, body, $"/projects/{project}/entities/{entityid}/data", query);
        }


        /// <summary>
        /// Gets an array of batteries at all active sites with last reading and days-to-low threshold predictions.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorBattery objects</returns>
        public MonitorBattery[] GetMonitorBatteryStatus()
        {
            return ApiCaller.GetResponseJson<MonitorBattery[]>(_auth, $"/sites/battery");
        }

        /// <summary>
        /// Gets all active alarms.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <returns>An array of MonitorAlarm objects</returns>
        public MonitorAlarm[] GetActiveAlarms(Guid project)
        {
            return ApiCaller.GetResponseJson<MonitorAlarm[]>(_auth, $"/projects/{project}/alarms");
        }


        /// <summary>
        /// Gets all active alarms.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="timeframe">The timeframe to look back in hours.</param>
        /// <returns>An array of MonitorAlarm objects</returns>
        public MonitorAlarm[] GetAlarms(Guid project, int timeframe)
        {
            return ApiCaller.GetResponseJson<MonitorAlarm[]>(_auth, $"/projects/{project}/alarms/{timeframe}");
        }

        /// <summary>
        /// Gets all work orders that match the filters provided.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="siteid">OPTIONAL: A site guid for filtering work orders by site.</param>
        /// <param name="status_filter">OPTIONAL: A list of statuses to filter for. By default, the query will return all open work orders.</param>
        /// <param name="modified_after">OPTIONAL: Gets work orders modified after a specific date.</param>
        /// <returns></returns>
        public WorkOrder[] GetWorkOrders(Guid project, Guid? siteid = null, WorkOrderStatus[] status_filter = null, DateTime? modified_after = null)
        {
            //Get the query parameters
            var query = new Dictionary<string, string>();
            if (siteid.HasValue) query.Add("siteid", siteid.ToString());
            if (status_filter != null) query.Add("status", string.Join(",", status_filter));
            if (modified_after.HasValue) query.Add("modified_after", FormatQueryDate(modified_after.Value));

            //Get the work orders
            return ApiCaller.GetResponseJson<WorkOrder[]>(_auth, $"/projects/{project}/workorders", query);
		}

        #endregion

        #region SSES
        /// <summary>
        /// Gets the attribute list for a given data type.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="recordtype">The data type such as "StructureInspection" or "SmokeObservation".</param>
        /// <returns>An array of DataAttribute objects.</returns>
        public DataAttribute[] GetDataAttributes(Guid project, string recordtype)
        {
            return ApiCaller.GetResponseJson<DataAttribute[]>(_auth, $"/projects/{project}/{recordtype}/attributes");
        }

        /// <summary>
        /// Gets the report group list for a given data type.
        /// </summary>
        /// <param name="project">The project guid.</param>
        /// <param name="recordtype">The data type such as "StructureInspection" or "SmokeObservation".</param>
        /// <returns>An array of DataReportGroup objects.</returns>
        public DataReportGroup[] GetDataReportGroups(Guid project, string recordtype)
        {
            return ApiCaller.GetResponseJson<DataReportGroup[]>(_auth, $"/projects/{project}/{recordtype}/reportgroups");
        }

        /// <summary>
        /// Gets a record list matching the filters with any number of additional attributes.
        /// </summary>
        /// <param name="recordtype"></param>
        /// <param name="projectid">Optional project guid filter.</param>
        /// <param name="created_after">Optional filter by created date.</param>
        /// <param name="modified_after">Optional filter for modified date.</param>
        /// <param name="attributes">Optionally request additional attributes to return with the query.</param>
        /// <returns>A data table with the records including the additional attribute columns.</returns>
        public DataTable GetDataRecordTable(string recordtype, Guid? projectid = null, DateTime? created_after = null, DateTime? modified_after = null, string[] attributes = null)
        {
       
            //Create the query
            var query = new Dictionary<string, string>();
            if (projectid.HasValue) query.Add("projectid", projectid.Value.ToString());
            if (created_after.HasValue) query.Add("created_after", FormatQueryDate(created_after.Value));
            if (modified_after.HasValue) query.Add("modified_after", FormatQueryDate(modified_after.Value));
            if (attributes != null && attributes.Length > 0) query.Add("attribute_list", string.Join(",", attributes));

            //Get the items
            var items = ApiCaller.GetResponseJson<Dictionary<string,object>[]>(_auth, $"/{recordtype}/list", query);

            //Convert to data table
            return Data.DataRecordsToDataTable(items, attributes);


        }

        /// <summary>
        /// Gets all filtered data in the GeoJSON format.
        /// </summary>
        /// <param name="recordtype"></param>
        /// <param name="projectid">Optional project guid filter.</param>
        /// <param name="created_after">Optional filter by created date.</param>
        /// <param name="modified_after">Optional filter for modified date.</param>
        /// <param name="attributes">Optionally request additional attributes to return with the query.</param>
        /// <returns>Returns a JSON string in the GeoJSON format.</returns>
        public string GetGeoJson(string recordtype, Guid? projectid = null, DateTime? created_after = null, DateTime? modified_after = null, string[] attributes = null)
        {

            //Create the query
            var query = new Dictionary<string, string>();
            if (projectid.HasValue) query.Add("projectid", projectid.Value.ToString());
            if (created_after.HasValue) query.Add("created_after", FormatQueryDate(created_after.Value));
            if (modified_after.HasValue) query.Add("modified_after", FormatQueryDate(modified_after.Value));
            if (attributes != null && attributes.Length > 0) query.Add("attribute_list", string.Join(",", attributes));

            //Get the json
            return ApiCaller.GetResponseJsonRaw(_auth, $"/{recordtype}/geojson", query);

        }

        /// <summary>
        /// Gets all related info for a data record.
        /// </summary>
        /// <param name="recordtype">The data type, such as "SmokeObservation" or "StructureInspection"</param>
        /// <param name="record_id">The record guid.</param>
        /// <param name="attribute_info">Return all attributes.</param>
        /// <param name="pretty_attributes">Use the human-friendly labels for the attributes rather than the name identifiers.</param>
        /// <param name="media_info">Return a list of related media.</param>
        /// <param name="coordinate_info">Return the location coordinates.</param>
        /// <param name="parent_info">Return related parent record if exists.</param>
        /// <param name="child_info">Return related child records (if they exist). This will recursively get all children and grandchildren.</param>
        /// <returns>Returns a DataRecordInfo object.</returns>
        public DataRecordInfo GetRecordInfo(string recordtype, Guid record_id, bool attribute_info = true, bool pretty_attributes = false, bool media_info = true, bool coordinate_info = true, bool parent_info = false, bool child_info = false)
        {
            //Create the query
            var query = new Dictionary<string, string>();
            query.Add("attribute_info", attribute_info.ToString().ToLower());
            query.Add("pretty_attributes", pretty_attributes.ToString().ToLower());
            query.Add("media_info", media_info.ToString().ToLower());
            query.Add("coordinate_info", coordinate_info.ToString().ToLower());
            query.Add("parent_info", parent_info.ToString().ToLower());
            query.Add("child_info", child_info.ToString().ToLower());

            //Get the item
            return ApiCaller.GetResponseJson<DataRecordInfo>(_auth, $"/{recordtype}/{record_id}/info", query);
        }

        #endregion

        #region "RDII"

        /// <summary>
        /// Gets the storm events that occured on the project.
        /// </summary>
        /// <param name="projectid">The project guid.</param>
        /// <param name="occur_after">Filter for storms that occured after this date.</param>
        /// <param name="occur_before">Filter for storms that occured before this date.</param>
        /// <returns></returns>
        public RdiiStorm[] GetStormEvents(Guid projectid, DateTime? occur_after = null, DateTime? occur_before = null)
        {

            //Create the query
            var query = new Dictionary<string, string>();
            if (occur_after.HasValue) query.Add("occur_after", FormatQueryDate(occur_after.Value));
            if (occur_before.HasValue) query.Add("occur_before", FormatQueryDate(occur_before.Value));

            //Get the items
            return ApiCaller.GetResponseJson<RdiiStorm[]>(_auth, $"/projects/{projectid}/storms", query);

        }

        /// <summary>
        /// Gets the rain takeoffs for the storm event.
        /// </summary>
        /// <param name="stormid"></param>
        /// <returns>Returns an array of RdiiRainTakeoff objects.</returns>
        public RdiiRainTakeoff[] GetRainTakeoffs(Guid stormid)
        {
            return ApiCaller.GetResponseJson<RdiiRainTakeoff[]>(_auth, $"/storms/{stormid}/takeoffs");
        }

        /// <summary>
        /// Gets rain takeoffs for a particular duration, looking back the specified number of days.
        /// </summary>
        /// <param name="projectid">The project guid.</param>
        /// <param name="duration">The rain intensity duration in minutes.</param>
        /// <param name="timeframe">The number of days to look back and query storm events.</param>
        /// <returns></returns>
        public RdiiRainTakeoff[] GetRainTakeoffsForDurationAndTimeframe(Guid projectid, int duration, int timeframe)
        {
            return ApiCaller.GetResponseJson<RdiiRainTakeoff[]>(_auth, $"/projects/{projectid}/storms/{duration}/takeoffs/{timeframe}");
        }
        #endregion

        #region Source Defect Analysis

        /// <summary>
        /// Gets a summary by defect including total I/I contribution and number of instances.
        /// </summary>
        /// <param name="projectid">The project guid.</param>
        /// <param name="inspection_types">A list of Inspection Type filters. (this can be optained using the GetRecordTypes() function.</param>
        /// <param name="defect_types">A list of defect filters.</param>
        /// <param name="modified_after">Filter for anything modified after this date.</param>
        /// <returns>Returns an array of SsesDefectSummary objects.</returns>
        public SsesDefectSummary[] GetDefectAnalysisSummary(Guid? projectid = null, string[] inspection_types = null, string[] defect_types = null, DateTime? modified_after = null)
        {
            //Create the query
            var query = new Dictionary<string, string>();
            if (projectid.HasValue) query.Add("projectid", projectid.Value.ToString());
            if (inspection_types != null && inspection_types.Length > 0) query.Add("inspection_types", string.Join(",", inspection_types));
            if (defect_types != null && defect_types.Length > 0) query.Add("defect_types", string.Join(",", defect_types));
            if (modified_after.HasValue) query.Add("modified_after", FormatQueryDate(modified_after.Value));

            return ApiCaller.GetResponseJson<SsesDefectSummary[]>(_auth, $"/defects/summary", query);
        }
        #endregion
    }
}
