using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaSync
{
	public class DataModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<string> LocalPathChanged;
		public event EventHandler<string> RemotePathChanged;
		public event EventHandler<string> UserNameChanged;


		private string _localPath;
		private string _remotePath;
		private string _userName;

		public string LocalPath
		{
			get
			{
				return _localPath;
			}
			set
			{
				if (value != _localPath)
				{
					_localPath = value;
				}
			}
		}
		public string RemotePath
		{
			get
			{
				return _remotePath;
			}
			set
			{
				if (_remotePath != value)
				{
					_remotePath = value;
				}
			}
		}
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				if (_userName != value)
				{
					_userName = value;
				}
			}
		}		

		protected virtual void OnLocalPathChanged()
		{
			LocalPathChanged?.Invoke(this, _localPath);
		}

		protected virtual void OnRemotePathChanged()
		{
			RemotePathChanged?.Invoke(this, _remotePath);
		}

		protected virtual void OnUserNameChanged()
		{
			UserNameChanged?.Invoke(this, _userName);
		}

		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
