namespace ReaSync
{
	partial class MainForm
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
			this.textBoxLocal = new System.Windows.Forms.TextBox();
			this.buttonBrowseLocal = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonBrowseRemote = new System.Windows.Forms.Button();
			this.textBoxRemote = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonCheckin = new System.Windows.Forms.Button();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonCheckout = new System.Windows.Forms.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonGetLatest = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxLocal
			// 
			this.textBoxLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLocal.Location = new System.Drawing.Point(22, 44);
			this.textBoxLocal.Name = "textBoxLocal";
			this.textBoxLocal.ReadOnly = true;
			this.textBoxLocal.Size = new System.Drawing.Size(358, 20);
			this.textBoxLocal.TabIndex = 0;
			// 
			// buttonBrowseLocal
			// 
			this.buttonBrowseLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseLocal.Location = new System.Drawing.Point(386, 42);
			this.buttonBrowseLocal.Name = "buttonBrowseLocal";
			this.buttonBrowseLocal.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseLocal.TabIndex = 1;
			this.buttonBrowseLocal.Text = "Browse...";
			this.buttonBrowseLocal.UseVisualStyleBackColor = true;
			this.buttonBrowseLocal.Click += new System.EventHandler(this.buttonBrowseLocal_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Local working folder:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Remote folder (Dropbox, OneDrive, NextCloud...):";
			// 
			// buttonBrowseRemote
			// 
			this.buttonBrowseRemote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseRemote.Location = new System.Drawing.Point(386, 97);
			this.buttonBrowseRemote.Name = "buttonBrowseRemote";
			this.buttonBrowseRemote.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseRemote.TabIndex = 4;
			this.buttonBrowseRemote.Text = "Browse...";
			this.buttonBrowseRemote.UseVisualStyleBackColor = true;
			this.buttonBrowseRemote.Click += new System.EventHandler(this.buttonBrowseRemote_Click);
			// 
			// textBoxRemote
			// 
			this.textBoxRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRemote.Location = new System.Drawing.Point(22, 99);
			this.textBoxRemote.Name = "textBoxRemote";
			this.textBoxRemote.ReadOnly = true;
			this.textBoxRemote.Size = new System.Drawing.Size(358, 20);
			this.textBoxRemote.TabIndex = 3;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(492, 266);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.textBoxLocal);
			this.tabPage1.Controls.Add(this.buttonBrowseRemote);
			this.tabPage1.Controls.Add(this.textBoxUserName);
			this.tabPage1.Controls.Add(this.buttonBrowseLocal);
			this.tabPage1.Controls.Add(this.textBoxRemote);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(484, 240);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Configuration";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 138);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Your user name:";
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUserName.Location = new System.Drawing.Point(22, 154);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(358, 20);
			this.textBoxUserName.TabIndex = 3;
			this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.buttonGetLatest);
			this.tabPage2.Controls.Add(this.buttonCheckin);
			this.tabPage2.Controls.Add(this.buttonRefresh);
			this.tabPage2.Controls.Add(this.buttonCheckout);
			this.tabPage2.Controls.Add(this.labelStatus);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(484, 240);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Synchronization";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// buttonCheckin
			// 
			this.buttonCheckin.Location = new System.Drawing.Point(260, 68);
			this.buttonCheckin.Name = "buttonCheckin";
			this.buttonCheckin.Size = new System.Drawing.Size(75, 23);
			this.buttonCheckin.TabIndex = 2;
			this.buttonCheckin.Text = "Check in";
			this.buttonCheckin.UseVisualStyleBackColor = true;
			this.buttonCheckin.Click += new System.EventHandler(this.buttonCheckin_Click);
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(17, 68);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 2;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// buttonCheckout
			// 
			this.buttonCheckout.Location = new System.Drawing.Point(179, 68);
			this.buttonCheckout.Name = "buttonCheckout";
			this.buttonCheckout.Size = new System.Drawing.Size(75, 23);
			this.buttonCheckout.TabIndex = 2;
			this.buttonCheckout.Text = "Check out";
			this.buttonCheckout.UseVisualStyleBackColor = true;
			this.buttonCheckout.Click += new System.EventHandler(this.buttonCheckout_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.AutoSize = true;
			this.labelStatus.Location = new System.Drawing.Point(83, 28);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(51, 13);
			this.labelStatus.TabIndex = 1;
			this.labelStatus.Text = "unknown";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Status:";
			// 
			// buttonGetLatest
			// 
			this.buttonGetLatest.Location = new System.Drawing.Point(98, 68);
			this.buttonGetLatest.Name = "buttonGetLatest";
			this.buttonGetLatest.Size = new System.Drawing.Size(75, 23);
			this.buttonGetLatest.TabIndex = 3;
			this.buttonGetLatest.Text = "Get Latest";
			this.buttonGetLatest.UseVisualStyleBackColor = true;
			this.buttonGetLatest.Click += new System.EventHandler(this.buttonGetLatest_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 266);
			this.Controls.Add(this.tabControl1);
			this.MinimumSize = new System.Drawing.Size(300, 225);
			this.Name = "MainForm";
			this.Text = "ReaSync";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxLocal;
		private System.Windows.Forms.Button buttonBrowseLocal;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonBrowseRemote;
		private System.Windows.Forms.TextBox textBoxRemote;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonCheckin;
		private System.Windows.Forms.Button buttonCheckout;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.Button buttonGetLatest;
	}
}

