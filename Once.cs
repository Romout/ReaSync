using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync
{
	public class Once : IDisposable
	{
		private static bool _active = false;
		private bool _oldValue;
		public Once()
		{
			_oldValue = _active;
			_active = true;
		}
		public static bool IsActive => _active;
		public void Dispose()
		{
			_active = _oldValue;
		}
	}
}
