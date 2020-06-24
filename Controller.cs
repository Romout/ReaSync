using Newtonsoft.Json;
using ReaSync.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaSync
{
	public enum StatusEnum
	{
		Unknown,
		Okay,
		Locked,
		CheckedOut,
		Error,
	}

	public enum ResultEnum
	{
		Success,
		Error,
		CheckedOutBySomeoneElse,
		NotCheckedOutByCaller,
	}

	public class Controller
	{
		private const string LOCK_FILE = ".lock";
		private const string HASH_FILE = ".hashes";

		private const string LOCAL_KEY = "local";
		private const string REMOTE_KEY = "remote";
		private const string USERNAME_KEY = "username";

		private DataModel _dataModel;
		private ISettings _settings;		

		public Controller(DataModel dataModel, ISettings settings)
		{
			_dataModel = dataModel;
			_settings = settings;

			_dataModel.LocalPath = _settings.GetSetting(LOCAL_KEY);
			_dataModel.RemotePath = _settings.GetSetting(REMOTE_KEY);
			_dataModel.UserName = _settings.GetSetting(USERNAME_KEY);
		}

		public void SaveSettings()
		{
			_settings.SetSetting(LOCAL_KEY, Model.LocalPath);
			_settings.SetSetting(REMOTE_KEY, Model.RemotePath);
			_settings.SetSetting(USERNAME_KEY, Model.UserName);
			_settings.Save();
		}

		public void SetStatus(StatusEnum status, string statusMessage)
		{
			Model.Status = status;
			Model.StatusMessage = statusMessage;
		}

		public void UpdateStatus()
		{
			Model.Status = StatusEnum.Error;
			if (!Directory.Exists(Model.LocalPath))
				Trace.WriteLine("Warning: Local path does not exist (should be created automatically)");

			if (Directory.Exists(Model.RemotePath))
			{
				SetStatus(StatusEnum.Okay, "okay");
				if (File.Exists(RemoteLockFile))
				{
					string name = File.ReadAllText(RemoteLockFile);
					if (name == Model.UserName)
					{
						SetStatus(StatusEnum.CheckedOut, $"You have checked out the files");
					}
					else
					{
						SetStatus(StatusEnum.Locked, $"Checked out by {name}");
					}
				}
			}
			else
				SetStatus(StatusEnum.Error, "Remote path does not exist");
		}

		public void GetLatestVersion()
		{
			Trace.WriteLine("Getting latest version");

			if (Directory.Exists(Model.LocalPath))
				Directory.Delete(Model.LocalPath, true);
			
			// That's odd but seems to can happen
			while (Directory.Exists(Model.LocalPath))
				Thread.Sleep(100);

			Directory.CreateDirectory(Model.LocalPath);

			// That's odd but seems to can happen
			while (!Directory.Exists(Model.LocalPath))
				Thread.Sleep(100);

			Copy(Model.RemotePath, Model.LocalPath);
		}

		public ResultEnum Checkout(Func<bool> queryOverwrite)
		{
			Trace.WriteLine("Check out");
			UpdateStatus();
			if (Model.Status == StatusEnum.Locked)
				return ResultEnum.CheckedOutBySomeoneElse;
			else if (Model.Status == StatusEnum.Okay ||
					(Model.Status == StatusEnum.CheckedOut && queryOverwrite()))
			{
				File.WriteAllText(RemoteLockFile, Model.UserName);
				GetLatestVersion();
				return ResultEnum.Success;
			}
			return ResultEnum.Error;
		}

		public ResultEnum Checkin()
		{
			Trace.WriteLine("Check in");
			UpdateStatus();

			if (Directory.Exists(Model.LocalPath) && Directory.Exists(Model.RemotePath))
			{
				if (Model.Status != StatusEnum.CheckedOut)
					return ResultEnum.NotCheckedOutByCaller;
				else
				{
					Dictionary<string, string> remoteHashes = new Dictionary<string, string>();
					Dictionary<string, string> localHashes = new Dictionary<string, string>();
					if (File.Exists(RemoteHashFile))
					{
						remoteHashes = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(RemoteHashFile));
					}
					foreach (string fileName in Directory.GetFiles(Model.LocalPath, "*", SearchOption.AllDirectories))
					{
						// Ignore hidden files
						if (Path.GetFileName(fileName).StartsWith("."))
							continue;

						string relativeFileName = PathExtended.GetRelativePath(Model.LocalPath, fileName);
						string localHash = FileChecksum(fileName);
						localHashes.Add(relativeFileName, localHash);

						if (!remoteHashes.TryGetValue(relativeFileName, out string remoteHash) || remoteHash != localHash)
						{
							string targetFile = Path.Combine(Model.RemotePath, relativeFileName);
							string targetPath = Path.GetDirectoryName(targetFile);
							if (!Directory.Exists(targetPath))
								Directory.CreateDirectory(targetPath);
							File.Copy(fileName, targetFile, true);
						}
					}
					File.WriteAllText(RemoteHashFile, JsonConvert.SerializeObject(localHashes, Formatting.Indented));
					File.Delete(RemoteLockFile);

					UpdateStatus();
					return ResultEnum.Success;
				}
			}
			return ResultEnum.Error;
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

			foreach (string fileName in Directory.GetFiles(source))
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

		public DataModel Model => _dataModel;
		public ISettings Settings => _settings;

		private string RemoteLockFile => Path.Combine(Model.RemotePath, LOCK_FILE);
		private string RemoteHashFile => Path.Combine(Model.RemotePath, HASH_FILE);

	}
}
