using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.ResponseObjects;
using System.Reflection;
using Sample_Clarity_API_App.UserInput;
using Clarity.RequestParameters;
using Clarity.Utilities;

namespace Sample_Clarity_API_App
{
	public partial class Main : Form
	{

		Clarity.Api _api = null;

		List<BindingSource> resultsCache = new List<BindingSource>();

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			//For Testing
			client_id.Text = "y9zEJii5";
			password.Text = "0.rwbb5x6ghk0.oasicj3ooch0.";
		}

		private void submit_button_Click(object sender, EventArgs e)
		{
			//Create the new API passing the credentials
			_api = new Clarity.Api(client_id.Text.Trim(), password.Text.Trim());

			//Wire up the api events
			_api.ClarityHttpCall += (object _sender, ClarityHttpCallEventArgs _e) => OutputHttpCall(_e.path);
			_api.ClarityHttpError += (object _sender, ClarityHttpCallErrorEventArgs _e) => MessageBox.Show(_e.e.Message + (_e.result.Length < 1000 ? "\n\n" + _e.result : ""));

			//Output the function call
			OutputFunction($"var api = new Clarity.Api(\"{client_id.Text.Trim()}\", \"{password.Text.Trim()}\");");

			//Validate and hand UI settings
			if (_api.Validate())
			{
				SetValidLogin();
			}
			else
			{
				SetInvalidLogin();
			}
		}

		private void SetInvalidLogin()
		{
			validationMsg.Text = "Invalid authorization.";
			validationMsg.ForeColor = Color.Red;
			dataGettersGroup.Enabled = false;
		}
	
		private void SetValidLogin()
		{
			validationMsg.Text = "Authorization valid.";
			validationMsg.ForeColor = Color.Green;
			dataGettersGroup.Enabled = true;
		}

		private void OutputFunction(string line)
		{
			txtFunctionCalls.Text += line += "\n\n";
			txtFunctionCalls.Select(txtFunctionCalls.Text.Length - 1, 0);
			txtFunctionCalls.ScrollToCaret();

		}

		private void OutputHttpCall(string line)
		{
			txtHttpCalls.Text += line += "\n";
			txtHttpCalls.Select(txtHttpCalls.Text.Length - 1, 0);
			txtHttpCalls.ScrollToCaret();
		}


		private void SetDataGrid(object items)
		{


			if (dataGrid.DataSource != null)
			{
				//Cache the old results
				BindingSource oldBinding = (BindingSource)dataGrid.DataSource;
				resultsCache.Add(oldBinding);
				back_button.Visible = true;
			}

			//Clear any old columns
			dataGrid.DataSource = null;
			dataGrid.Columns.Clear();


			//Handle the UI
			if (items is Array || items is DataTable)
			{
				richTextBox1.Visible = false;
				richTextBox1.Dock = DockStyle.None;

				dataGrid.Visible = true;
				dataGrid.Dock = DockStyle.Fill;

				//Set the new bindings
				dataGrid.AutoGenerateColumns = true;
				var b = new BindingSource();
				b.DataSource = items;
				b.ResetBindings(true);
				dataGrid.DataSource = b;

				//Include the Drill-down buttons
				ExtendDataGridWithDrillDowns(items);
			}
			else
			{
				dataGrid.Visible = false;
				dataGrid.Dock = DockStyle.None;

				richTextBox1.Visible = true;
				richTextBox1.Dock = DockStyle.Fill;

				richTextBox1.Text = items.ToString();
			}



		}

		private void SetDataGridBack()
		{
			if (resultsCache.Count > 0)
			{
				//Ensure UI is back to the data grid
				richTextBox1.Visible = false;
				richTextBox1.Dock = DockStyle.None;
				dataGrid.Visible = true;
				dataGrid.Dock = DockStyle.Fill;

				//Get the last result
				var result = resultsCache.Last();

				//Remove from the cache
				resultsCache.Remove(result);

				//Hide the button if we are all the way back
				if (resultsCache.Count == 0) back_button.Visible = false;

				//Clear any old columns
				dataGrid.DataSource = null;
				dataGrid.Columns.Clear();

				//Set the datasource
				dataGrid.DataSource = result;
				ExtendDataGridWithDrillDowns(result.DataSource);
			}
		}

		private void ExtendDataGridWithDrillDowns(object items)
		{
			if (items is Array)
			{
				var arr = ((object[])items);
				if (arr?.Length > 0)
				{
					//Get the methods on the class
					var methods = arr.First().GetType().GetMethods().Where(m => !m.IsSpecialName && !m.IsVirtual && m.Name != "GetType");
					foreach (var method in methods)
					{
						//Add a button column for each method
						var button = new DataGridViewButtonColumn();
						button.HeaderText = method.Name.Replace("Get", "");
						button.Name = method.Name;
						dataGrid.Columns.Add(button);
					}
				}
			}
		}

		private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//Get the clicked cell
			var cell = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (cell is DataGridViewButtonCell)
			{
				//Get the method name which will just be the column name
				var methodName = cell.OwningColumn.Name;

				//Get the bound item
				var item = cell.OwningRow.DataBoundItem;

				try
				{
					//See what parameters the method has
					var method = item.GetType().GetMethod(methodName);

					//Initialize the method parameters with just the api object			
					object[] prms = { _api };

					//Get user input if necessary to get the params
					var fields = item.GetType().GetFields();
					var requestParams = (IRequestParameters)item.GetType().GetFields().SingleOrDefault(f => f.Name == methodName + "RequestParameters")?.GetValue(item);
					if (requestParams != null) 
					{
						prms = UserInputUtil.GetUserParameters(_api, item, requestParams);
					}

					//Show the function calls
					var itemClassName = item.GetType().Name;
					OutputFunction($"var {itemClassName}{e.RowIndex} = {itemClassName}Results[{e.RowIndex}];");
					OutputFunction($"var {method.ReturnType.Name.Replace("[]", "")}Results = {itemClassName}{e.RowIndex}.{methodName}({string.Join(",", prms.Select(p => ParameterToString(p)))});");

					//Call the method and get the results
					var results = method.Invoke(item, prms);

					//Put the new results on the data grid
					if (results != null) SetDataGrid(results);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

			}
		}

		private string ParameterToString(object p)
		{
			if (p == null)
			{
				return "null";
			}
			else if (p is Enum)
			{
				return p.GetType().Name + "." + p.ToString();
			}
			else if (p is Clarity.Api) 
			{
				return "api";
			}
			else if (p is string || p is DateTime)
			{
				return "\"" + p.ToString() + "\"";
			}
			else if (p is Array)
			{
				object[] arr = (object[])p;
				return "new " + p.ToString() + " { " + string.Join(",", arr.Select(a => ParameterToString(a))) + " }";
			}
			else
			{
				return p.ToString();
			}
		}

		private void projects_Click(object sender, EventArgs e)
		{
			var items = _api.GetProjects();
			OutputFunction($"var ProjectResults = api.GetProjects();");
			SetDataGrid(items);
		}

		private void project_status_Click(object sender, EventArgs e)
		{
			var items = _api.GetProjectProgress();
			OutputFunction($"var ProjectProgressResults = api.GetProjectProgress();");
			if (items != null) SetDataGrid(items);
		}
		private void button1_Click(object sender, EventArgs e)
		{
			var items = _api.GetMonitorBatteryStatus();
			OutputFunction($"var MonitorBatteryResults = api.GetMonitorBatteryStatus();");
			if (items != null) SetDataGrid(items);
		}

		private void back_button_Click(object sender, EventArgs e)
		{
			SetDataGridBack();
		}

		private void recordinfo_button_Click(object sender, EventArgs e)
		{
			var request = new Clarity.RequestParameters.RecordInfoRequestParameters();
			var prms = UserInputUtil.GetUserParameters(_api, null, request);
			var item = _api.GetRecordInfo(request.datatype, request.record_id, request.attribute_info, request.pretty_attributes, request.media_info, request.coordinate_info, request.parent_info, request.child_info);
			if (item != null) SetDataGrid(item);
		}
	}
}
