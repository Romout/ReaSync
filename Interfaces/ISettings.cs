using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync.Interfaces
{
	public interface ISettings
	{
		string GetSetting(string name);
		void SetSetting(string name, string value);
	}
}
