using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Clarity_API_App.UserInput
{
	public partial class ParametersDialog : Form
	{

		public ParametersDialog()
		{
			InitializeComponent();
		}

		object _props;
		public dynamic GetUserInput(object param)
		{
			_props = param;
			ShowDialog();
			return param;
		}

		private void ParametersDialog_Load(object sender, EventArgs e)
		{
			propertyGrid1.SelectedObject = _props;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();

		}

		private void ParametersDialog_Shown(object sender, EventArgs e)
		{
			
		}
	}
}
