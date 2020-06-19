using ReaSync.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync
{
	public class Controller
	{
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


		public DataModel Model => _dataModel;
		public ISettings Settings => _settings;
	}
}
