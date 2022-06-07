
namespace Sample_Clarity_API_App
{
	partial class Main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGettersGroup = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.back_button = new System.Windows.Forms.Button();
			this.project_status = new System.Windows.Forms.Button();
			this.projects = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.validationMsg = new System.Windows.Forms.Label();
			this.submit_button = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.client_id = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtFunctionCalls = new System.Windows.Forms.RichTextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtHttpCalls = new System.Windows.Forms.RichTextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.dataGettersGroup.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1017, 594);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.AutoScrollMinSize = new System.Drawing.Size(0, 1000);
			this.panel1.Controls.Add(this.dataGettersGroup);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(194, 588);
			this.panel1.TabIndex = 0;
			// 
			// dataGettersGroup
			// 
			this.dataGettersGroup.Controls.Add(this.button1);
			this.dataGettersGroup.Controls.Add(this.back_button);
			this.dataGettersGroup.Controls.Add(this.project_status);
			this.dataGettersGroup.Controls.Add(this.projects);
			this.dataGettersGroup.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGettersGroup.Enabled = false;
			this.dataGettersGroup.Location = new System.Drawing.Point(0, 154);
			this.dataGettersGroup.Name = "dataGettersGroup";
			this.dataGettersGroup.Size = new System.Drawing.Size(177, 301);
			this.dataGettersGroup.TabIndex = 1;
			this.dataGettersGroup.TabStop = false;
			this.dataGettersGroup.Text = "Data Getters";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 77);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(162, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Battery Status";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// back_button
			// 
			this.back_button.Location = new System.Drawing.Point(6, 135);
			this.back_button.Name = "back_button";
			this.back_button.Size = new System.Drawing.Size(162, 23);
			this.back_button.TabIndex = 2;
			this.back_button.Text = "< Back";
			this.back_button.UseVisualStyleBackColor = true;
			this.back_button.Visible = false;
			this.back_button.Click += new System.EventHandler(this.back_button_Click);
			// 
			// project_status
			// 
			this.project_status.Location = new System.Drawing.Point(6, 48);
			this.project_status.Name = "project_status";
			this.project_status.Size = new System.Drawing.Size(162, 23);
			this.project_status.TabIndex = 1;
			this.project_status.Text = "Project Status";
			this.project_status.UseVisualStyleBackColor = true;
			this.project_status.Click += new System.EventHandler(this.project_status_Click);
			// 
			// projects
			// 
			this.projects.Location = new System.Drawing.Point(9, 19);
			this.projects.Name = "projects";
			this.projects.Size = new System.Drawing.Size(162, 23);
			this.projects.TabIndex = 0;
			this.projects.Text = "Projects";
			this.projects.UseVisualStyleBackColor = true;
			this.projects.Click += new System.EventHandler(this.projects_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.validationMsg);
			this.groupBox1.Controls.Add(this.submit_button);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.password);
			this.groupBox1.Controls.Add(this.client_id);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(177, 154);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Credentials";
			// 
			// validationMsg
			// 
			this.validationMsg.AutoSize = true;
			this.validationMsg.ForeColor = System.Drawing.Color.DimGray;
			this.validationMsg.Location = new System.Drawing.Point(9, 133);
			this.validationMsg.Name = "validationMsg";
			this.validationMsg.Size = new System.Drawing.Size(77, 13);
			this.validationMsg.TabIndex = 5;
			this.validationMsg.Text = "Not Authorized";
			// 
			// submit_button
			// 
			this.submit_button.Location = new System.Drawing.Point(9, 104);
			this.submit_button.Name = "submit_button";
			this.submit_button.Size = new System.Drawing.Size(162, 23);
			this.submit_button.TabIndex = 4;
			this.submit_button.Text = "Submit";
			this.submit_button.UseVisualStyleBackColor = true;
			this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Client ID";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(9, 75);
			this.password.Name = "password";
			this.password.Size = new System.Drawing.Size(162, 20);
			this.password.TabIndex = 1;
			// 
			// client_id
			// 
			this.client_id.Location = new System.Drawing.Point(9, 32);
			this.client_id.Name = "client_id";
			this.client_id.Size = new System.Drawing.Size(162, 20);
			this.client_id.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(203, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(811, 588);
			this.splitContainer1.SplitterDistance = 345;
			this.splitContainer1.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.panel2);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(811, 345);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Results";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.dataGrid);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel2.Location = new System.Drawing.Point(3, 18);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(805, 324);
			this.panel2.TabIndex = 2;
			// 
			// dataGrid
			// 
			this.dataGrid.AllowUserToAddRows = false;
			this.dataGrid.AllowUserToDeleteRows = false;
			this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.Size = new System.Drawing.Size(805, 324);
			this.dataGrid.TabIndex = 1;
			this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
			this.splitContainer2.Size = new System.Drawing.Size(811, 239);
			this.splitContainer2.SplitterDistance = 412;
			this.splitContainer2.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtFunctionCalls);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(410, 237);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Function Calls";
			// 
			// txtFunctionCalls
			// 
			this.txtFunctionCalls.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFunctionCalls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFunctionCalls.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFunctionCalls.Location = new System.Drawing.Point(3, 18);
			this.txtFunctionCalls.Name = "txtFunctionCalls";
			this.txtFunctionCalls.ReadOnly = true;
			this.txtFunctionCalls.Size = new System.Drawing.Size(404, 216);
			this.txtFunctionCalls.TabIndex = 0;
			this.txtFunctionCalls.Text = "";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtHttpCalls);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(393, 237);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "HTTP Calls";
			// 
			// txtHttpCalls
			// 
			this.txtHttpCalls.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtHttpCalls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtHttpCalls.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtHttpCalls.Location = new System.Drawing.Point(3, 18);
			this.txtHttpCalls.Name = "txtHttpCalls";
			this.txtHttpCalls.ReadOnly = true;
			this.txtHttpCalls.Size = new System.Drawing.Size(387, 216);
			this.txtHttpCalls.TabIndex = 1;
			this.txtHttpCalls.Text = "";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1017, 594);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Main";
			this.ShowIcon = false;
			this.Text = "Sample Clarity Integration App";
			this.Load += new System.EventHandler(this.Main_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.dataGettersGroup.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox dataGettersGroup;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox client_id;
		private System.Windows.Forms.Button submit_button;
		private System.Windows.Forms.Label validationMsg;
		private System.Windows.Forms.Button project_status;
		private System.Windows.Forms.Button projects;
		private System.Windows.Forms.DataGridView dataGrid;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RichTextBox txtFunctionCalls;
		private System.Windows.Forms.RichTextBox txtHttpCalls;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button back_button;
		private System.Windows.Forms.Button button1;
	}
}

