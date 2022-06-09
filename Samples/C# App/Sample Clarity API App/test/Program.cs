using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			var api = new Clarity.Api("", "");

			var projects = api.GetProjects();

			var MyProject = projects.Single((p) => p.projectnumber == "[My Project Number like XX-XXXX-XX]");
			var sites = MyProject.GetMonitorSites(api);
		}
	}
}
