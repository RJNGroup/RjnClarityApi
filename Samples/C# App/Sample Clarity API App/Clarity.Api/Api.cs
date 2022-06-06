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

    }
}
