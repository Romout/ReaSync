using System;
using System.Linq;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading;

namespace ReaSync
{
	public partial class MainForm : Form
	{
		private enum StatusEnum
		{
			Unknown,
			Okay,
			Locked,
			CheckedOut,
			Error,
		}

		private const string APP_NAME = "ReaSync";
		private const string LOCK_FILE = ".lock";
		private const string HASH_FILE = ".hashes";
		private const string LOCAL_KEY = "local";
		private const string REMOTE_KEY = "remote";
		private const string USERNAME_KEY = "username";

		private string _localPath;
		private string _remotePath;
		private Configuration _config;

		public MainForm()
		{
			InitializeComponent();

			_config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			LocalPath = GetSetting(LOCAL_KEY).Value;
			RemotePath = GetSetting(REMOTE_KEY).Value;
			UserName = GetSetting(USERNAME_KEY).Value;
			if (string.IsNullOrEmpty(UserName))
				UserName = Environment.UserName;

			using (new Once())
				textBoxUserName.Text = UserName;

			UpdateStatus();
		}

		private void buttonBrowseLocal_Click(object sender, EventArgs e)
		{
			LocalPath = Browse(textBoxLocal.Text);
			SaveSettings();
		}

		private void buttonBrowseRemote_Click(object sender, EventArgs e)
		{
			RemotePath = Browse(textBoxRemote.Text);
			SaveSettings();
		}

		private KeyValueConfigurationElement GetSetting(string name)
		{
			if (!_config.AppSettings.Settings.AllKeys.Contains(name))
				_config.AppSettings.Settings.Add(name, "");
			return _config.AppSettings.Settings[name];
		}
		private void SaveSettings()
		{
			GetSetting(LOCAL_KEY).Value = LocalPath;
			GetSetting(REMOTE_KEY).Value = RemotePath;
			GetSetting(USERNAME_KEY).Value = UserName;
			_config.Save(ConfigurationSaveMode.Modified);
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

		private void UpdateStatus()
		{
			Status = StatusEnum.Error;
			if (Directory.Exists(RemotePath))
			{
				SetStatus("okay");
				Status = StatusEnum.Okay;
				if (File.Exists(RemoteLockFile))
				{
					string name = File.ReadAllText(RemoteLockFile);
					if (name == UserName)
					{
						SetStatus($"You have checked out the files");
						Status = StatusEnum.CheckedOut;
					}
					else
					{
						SetStatus($"Checked out by {name}");
						Status = StatusEnum.Locked;
					}
				}
			}
			else
				SetStatus("Remote path does not exist");
		}

		private void SetStatus(string message) => labelStatus.Text = message;
		private void ShowError(string message) => MessageBox.Show(message, APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
		private bool Query(string message) => MessageBox.Show(message, APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		private void buttonGetLatest_Click(object sender, EventArgs e)
		{
			if (Query("Are you sure you want to overwrite your local files with the state of the remote path?"))
				GetLatestVersion();
		}

		private void buttonCheckout_Click(object sender, EventArgs e)
		{
			UpdateStatus();
			if (Status == StatusEnum.Locked)
				ShowError("Files are currently locked by someone else");
			else if (Status == StatusEnum.Okay ||
					(Status == StatusEnum.CheckedOut && Query("You've already got the files checked out. Are you sure you want to overwrite your local files with the state of the remote path?")))
			{
				File.WriteAllText(RemoteLockFile, UserName);
				GetLatestVersion();
			}
		}

		private void GetLatestVersion()
		{ 
			Directory.Delete(LocalPath, true);
			Directory.CreateDirectory(LocalPath);

			// That's odd but seems to can happen
			while (!Directory.Exists(LocalPath))
				Thread.Sleep(100);

			Copy(RemotePath, LocalPath);
		}

		private void buttonCheckin_Click(object sender, EventArgs e)
		{
			UpdateStatus();

			if (Directory.Exists(LocalPath) && Directory.Exists(RemotePath))
			{
				if (Status != StatusEnum.CheckedOut)
					ShowError("The files are currently not checked out by you");
				else
				{
					Dictionary<string, string> remoteHashes = new Dictionary<string, string>();
					Dictionary<string, string> localHashes = new Dictionary<string, string>();
					if (File.Exists(RemoteHashFile))
					{
						remoteHashes = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(RemoteHashFile));
					}
					foreach (string fileName in Directory.GetFiles(LocalPath, "*", SearchOption.AllDirectories))
					{
						// Ignore hidden files
						if (Path.GetFileName(fileName).StartsWith("."))
							continue;

						string relativeFileName = PathExtended.GetRelativePath(LocalPath, fileName);
						string localHash = FileChecksum(fileName);
						localHashes.Add(relativeFileName, localHash);

						if (!remoteHashes.TryGetValue(relativeFileName, out string remoteHash) || remoteHash != localHash)
						{
							string targetFile = Path.Combine(RemotePath, relativeFileName);
							string targetPath = Path.GetDirectoryName(targetFile);
							if (!Directory.Exists(targetPath))
								Directory.CreateDirectory(targetPath);
							File.Copy(fileName, targetFile, true);
						}
					}
					File.WriteAllText(RemoteHashFile, JsonConvert.SerializeObject(localHashes, Formatting.Indented));
					File.Delete(RemoteLockFile);

					UpdateStatus();
				}
			}
		}

		private string FileChecksum(string fileName)
		{
			using (var stream = new BufferedStream(File.OpenRead(fileName), 1200000))
			{
				MD5Cng md5 = new MD5Cng();
				byte[] checksum = md5.ComputeHash(stream);
				return BitConverter.ToString(checksum).Replace("-", string.Empty);
			}
		}

		private void Copy(string source, string destination)
		{
			if (!Directory.Exists(destination))
				Directory.CreateDirectory(destination);

			foreach(string fileName in Directory.GetFiles(source))
			{
				if (Path.GetFileName(fileName).StartsWith("."))
					continue;

				File.Copy(fileName, Path.Combine(destination, Path.GetFileName(fileName)), true);
			}

			foreach (string directory in Directory.GetDirectories(source))
			{
				Copy(directory, Path.Combine(destination, Path.GetFileName(directory)));
			}
		}

		private string RemoteLockFile => Path.Combine(_remotePath, LOCK_FILE);
		private string RemoteHashFile => Path.Combine(_remotePath, HASH_FILE);
		private string LocalPath
		{
			get => _localPath;
			set
			{
				if (_localPath != value)
				{
					_localPath = value;
					textBoxLocal.Text = _localPath;
				}
			}
		}
		private string RemotePath
		{
			get => _remotePath;
			set
			{
				if (_remotePath != value)
				{
					_remotePath = value;
					textBoxRemote.Text = _remotePath;
				}
			}
		}

		public string UserName { get; private set; }
		private StatusEnum Status { get; set; }

		private void textBoxUserName_TextChanged(object sender, EventArgs e)
		{
			if (Once.IsActive)
				return;

			UserName = textBoxUserName.Text;
			SaveSettings();
		}

	}
}
