using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync
{
	public class ActionTraceListener : TraceListener
	{
		private Action<string, bool> _logFunc;

		public ActionTraceListener(Action<string, bool> logFunc)
		{
			_logFunc = logFunc;
		}

		public override void Write(string message)
		{
			_logFunc(message, true);
		}

		public override void WriteLine(string message)
		{
			_logFunc(message, false);
		}
	}
}
