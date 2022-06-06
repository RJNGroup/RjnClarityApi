using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Utilities;
using Clarity.ResponseObjects;

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

        private Dictionary<Guid, List<MonitorSite>> _SiteProjectMap = new Dictionary<Guid, List<MonitorSite>>();

        /// <summary>
        /// Gets an array of flow meter site entities (raw sensor data or calculated measurement entities).
        /// </summary>
        /// <param name="project"></param>
        /// <returns>An array of MonitorSiteEntity objects</returns>
        public MonitorSiteEntity[] GetMonitorEntities(Guid? project, Guid site)
        {
            Guid _project;
            if (project.HasValue) {
                _project = project.Value;
            }
            else 
            {
                _project = _SiteProjectMap.SingleOrDefault(k => k.Value.SingleOrDefault(s => s.id == site) != null).Key;
            }
            return ApiCaller.GetResponseJson<MonitorSiteEntity[]>(_auth, $"/projects/{_project}/sites/{site}/entities");
        }
        #endregion
    }
}
