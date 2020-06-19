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
		private DataModel _dataModel;
		private ISettings _settings;		

		public Controller(DataModel dataModel, ISettings settings)
		{
			_dataModel = dataModel;
			_settings = settings;
		}

		public ISettings Settings => _settings;
	}
}
