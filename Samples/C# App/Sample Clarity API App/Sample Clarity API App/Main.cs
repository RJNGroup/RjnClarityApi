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

namespace Sample_Clarity_API_App
{
	public partial class Main : Form
	{

		Clarity.Api _api = null;

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
			_api = new Clarity.Api(client_id.Text.Trim(), password.Text.Trim());
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

		private void projects_Click(object sender, EventArgs e)
		{
			var items = _api.GetProjects();
			SetDataGrid(items);
		}

		private void project_status_Click(object sender, EventArgs e)
		{
			var items = _api.GetProjectProgress();
			SetDataGrid(items);
		}

		private void SetDataGrid(object[] items)
		{
			//Clear any old columns
			dataGrid.DataSource = null;
			dataGrid.Columns.Clear();

			//Set the new bindings
			dataGrid.AutoGenerateColumns = true;
			var b = new BindingSource();
			b.DataSource = items;
			b.ResetBindings(true);
			dataGrid.DataSource = b;

			//Include the Drill-down buttons
			ExtendDataGridWithDrillDowns(items);
		}

		private void ExtendDataGridWithDrillDowns(object[] items)
		{
			if (items.Length > 0)
			{
				//Get the methods on the class
				var methods = items.First().GetType().GetMethods().Where(m => !m.IsSpecialName && !m.IsVirtual && m.Name != "GetType");
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

		private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//Get the clicked cell
			var cell = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (cell is DataGridViewButtonCell)
			{
				//Get the method name which will just be the column name
				var method = cell.OwningColumn.Name;

				//Get the bound item
				var item = cell.OwningRow.DataBoundItem;

				try
				{   //Call the new data getter method on the item and get the results
					object[] prms = { _api };
					object[] results = (object[])item.GetType().GetMethod(method).Invoke(item, prms);

					//Put the new results on the data grid
					SetDataGrid(results);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

			}
		}
	}
}
