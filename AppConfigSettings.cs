using ReaSync.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync
{
	public class AppConfigSettings : ISettings
	{
		private Configuration _config;

		public AppConfigSettings()
		{
			_config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		}

		private KeyValueConfigurationElement GetConfigurationElement(string name)
		{
			if (!_config.AppSettings.Settings.AllKeys.Contains(name))
				_config.AppSettings.Settings.Add(name, "");
			return _config.AppSettings.Settings[name];
		}

		public string GetSetting(string name)
		{
			return GetConfigurationElement(name).Value;
		}

		public void SetSetting(string name, string value)
		{
			GetConfigurationElement(name).Value = value;
		}
	}
}
