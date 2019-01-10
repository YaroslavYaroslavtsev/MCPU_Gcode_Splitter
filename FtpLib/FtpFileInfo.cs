using System;
using System.IO;

namespace FtpLib
{
	public class FtpFileInfo : FileSystemInfo
	{
		private FtpConnection _ftp;

		private string _filePath;

		private string _name;

		private DateTime? _lastAccessTime;

		private DateTime? _creationTime;

		private DateTime? _lastWriteTime;

		private FileAttributes _attribues;

		public FtpConnection FtpConnection
		{
			get
			{
				return this._ftp;
			}
		}

		public new DateTime? LastAccessTime
		{
			get
			{
				if (!this._lastAccessTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._lastAccessTime.Value);
			}
			internal set
			{
				this._lastAccessTime = value;
			}
		}

		public new DateTime? CreationTime
		{
			get
			{
				if (!this._creationTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._creationTime.Value);
			}
			internal set
			{
				this._creationTime = value;
			}
		}

		public new DateTime? LastWriteTime
		{
			get
			{
				if (!this._lastWriteTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._lastWriteTime.Value);
			}
			internal set
			{
				this._lastWriteTime = value;
			}
		}

		public new DateTime? LastAccessTimeUtc
		{
			get
			{
				if (!this._lastAccessTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._lastAccessTime.Value.ToUniversalTime());
			}
		}

		public new DateTime? CreationTimeUtc
		{
			get
			{
				if (!this._creationTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._creationTime.Value.ToUniversalTime());
			}
		}

		public new DateTime? LastWriteTimeUtc
		{
			get
			{
				if (!this._lastWriteTime.HasValue)
				{
					return null;
				}
				return new DateTime?(this._lastWriteTime.Value.ToUniversalTime());
			}
		}

		public new FileAttributes Attributes
		{
			get
			{
				return this._attribues;
			}
			internal set
			{
				this._attribues = value;
			}
		}

		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		public override bool Exists
		{
			get
			{
				return this.FtpConnection.FileExists(this.FullName);
			}
		}

		public FtpFileInfo(FtpConnection ftp, string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.OriginalPath = filePath;
			this.FullPath = filePath;
			this._filePath = filePath;
			this._ftp = ftp;
			this._name = Path.GetFileName(filePath);
		}

		public override void Delete()
		{
			this.FtpConnection.RemoveFile(this.FullName);
		}
	}
}
