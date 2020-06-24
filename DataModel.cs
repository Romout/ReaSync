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
		public event EventHandler<StatusEnum> StatusChanged;
		public event EventHandler<string> StatusMessageChanged;


		private string _localPath;
		private string _remotePath;
		private string _userName;
		private string _statusMessage;
		private StatusEnum _status;

		public string StatusMessage
		{
			get
			{
				return _statusMessage;
			}
			set
			{
				if (value != _statusMessage)
				{
					_statusMessage = value;
					OnStatusMessageChanged();
					OnPropertyChanged();
				}
			}
		}

		public StatusEnum Status
		{
			get
			{
				return _status;
			}
			set
			{
				if (value != _status)
				{
					_status = value;
					OnStatusChanged();
					OnPropertyChanged();
				}
			}
		}

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
					OnLocalPathChanged();
					OnPropertyChanged();
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
					OnRemotePathChanged();
					OnPropertyChanged();
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
					OnUserNameChanged();
					OnPropertyChanged();
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

		protected virtual void OnStatusChanged()
		{
			StatusChanged?.Invoke(this, _status);
		}

		protected virtual void OnStatusMessageChanged()
		{
			StatusMessageChanged?.Invoke(this, _statusMessage);
		}

		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
