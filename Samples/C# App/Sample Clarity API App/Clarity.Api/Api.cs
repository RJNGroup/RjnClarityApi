using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Utilities;
using Clarity.ResponseObjects;
using Clarity.Enums;

namespace Clarity
{
    public class Api
    {

        private Authorizer _auth;
        public Authorizer Authorizer { get { return _auth; } }

        public Api(string client_id, string password)
        {
            _auth = new Authorizer(client_id, password);
        }

        public bool Validate()
        {
            return _auth.Validate();
        }

        #region General

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
        /// <param name="project"></param>
        /// <returns>An array of DataType objects</returns>
        public DataType[] GetDataTypes(Guid project)
        {
            return ApiCaller.GetResponseJson<DataType[]>(_auth, $"/projects/{project}/datatypes");
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
            query.Add("from", from.ToString("yyyy-MM-ddTHH:mm:00.000"));
            if (to.HasValue) query.Add("to", to.Value.ToString("yyyy-MM-ddTHH:mm:00.000"));
            if (unitid > 0) query.Add("unit", unitid.ToString());
            query.Add("interval", ((int)interval).ToString());
            query.Add("aggregation", aggregation.ToString());

            //Get the result
            var result = ApiCaller.GetResponseJson<Dictionary<DateTime, double>>(_auth, $"/projects/{project}/entities/{entityid}/data", query);

            //Transform the result
            return result.Select(k => new MonitorDataPoint(k.Key, k.Value)).ToArray();
        }

        #endregion
    }
}
