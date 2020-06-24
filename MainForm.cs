using System;
using System.Linq;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;

namespace ReaSync
{
	public partial class MainForm : Form
	{
		private const string APP_NAME = "ReaSync";
		private const string LOCK_FILE = ".lock";
		private const string HASH_FILE = ".hashes";

		private ActionTraceListener _traceListener;

		public MainForm()
		{
			InitializeComponent();

			_traceListener = new ActionTraceListener(Log);
			Trace.Listeners.Add(_traceListener);
		}

		private DataModel Model => Controller.Model;

		private void InternalDispose()
		{
			Trace.Listeners.Remove(_traceListener);

			Model.StatusMessageChanged -= Model_StatusMessageChanged;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			dataModelBindingSource.DataSource = Model;

			if (string.IsNullOrEmpty(Model.UserName))
				Model.UserName = Environment.UserName;

			Model.StatusMessageChanged += Model_StatusMessageChanged;
			Controller.UpdateStatus();
		}

		private void Model_StatusMessageChanged(object sender, string message)
		{
			labelStatus.Text = message;
			Trace.WriteLine($"Status: {message}");
		}

		private void buttonBrowseLocal_Click(object sender, EventArgs e)
		{
			Model.LocalPath = Browse(Model.LocalPath);
			Controller.SaveSettings();
		}

		private void buttonBrowseRemote_Click(object sender, EventArgs e)
		{
			Model.RemotePath = Browse(Model.RemotePath);
			Controller.SaveSettings();
		}

		private string Browse(string currentFolder)
		{ 
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = currentFolder;
				if (dialog.ShowDialog(this) == DialogResult.OK)
					return dialog.SelectedPath;
			}
			return null;
		}

		private void ShowError(string message) => MessageBox.Show(message, APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
		private bool Query(string message) => MessageBox.Show(message, APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
		private void Log(string message, bool append)
		{
			if (append && listBoxLog.Items.Count > 0)
			{
				string currentMessage = (string)listBoxLog.Items[listBoxLog.Items.Count - 1];
				listBoxLog.Items[listBoxLog.Items.Count - 1] = $"{currentMessage}{message}";
			}
			else
			{
				listBoxLog.Items.Add(message);
				listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			Controller.UpdateStatus();
		}

		private void buttonGetLatest_Click(object sender, EventArgs e)
		{
			if (Query("Are you sure you want to overwrite your local files with the state of the remote path?"))
				Controller.GetLatestVersion();
		}

		private void buttonCheckout_Click(object sender, EventArgs e)
		{
			ResultEnum result = Controller.Checkout(() => Query("You've already got the files checked out. Are you sure you want to overwrite your local files with the state of the remote path?"));
			if (result == ResultEnum.CheckedOutBySomeoneElse)
				ShowError("Files are currently locked by someone else");
		}

		private void buttonCheckin_Click(object sender, EventArgs e)
		{
			if (Controller.Checkin() == ResultEnum.NotCheckedOutByCaller)
				ShowError("The files are currently not checked out by you");
		}

		private void textBoxUserName_TextChanged(object sender, EventArgs e)
		{
			if (Once.IsActive)
				return;

			Model.UserName = textBoxUserName.Text;
			Controller.SaveSettings();
		}

		public Controller Controller { get; set; }
	}
}
