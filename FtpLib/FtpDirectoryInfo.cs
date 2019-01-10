using System;
using System.IO;

namespace FtpLib
{
	public class FtpDirectoryInfo : FileSystemInfo
	{
		private FtpConnection _ftp;

		private string _dirPath;

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

		public override bool Exists
		{
			get
			{
				return this.FtpConnection.DirectoryExists(this.FullName);
			}
		}

		public override string Name
		{
			get
			{
				return Path.GetFileName(this.FullPath);
			}
		}

		public FtpDirectoryInfo(FtpConnection ftp, string path)
		{
			this._ftp = ftp;
			this.FullPath = path;
		}

		public override void Delete()
		{
			try
			{
				this._ftp.RemoveDirectory(this.Name);
			}
			catch (FtpException innerException)
			{
				throw new Exception("Unable to delete directory.", innerException);
			}
		}

		public FtpDirectoryInfo[] GetDirectories()
		{
			return this.FtpConnection.GetDirectories(this.FullPath);
		}

		public FtpDirectoryInfo[] GetDirectories(string path)
		{
			path = Path.Combine(this.FullPath, path);
			return this.FtpConnection.GetDirectories(path);
		}

		public FtpFileInfo[] GetFiles()
		{
			return this.GetFiles(this.FtpConnection.GetCurrentDirectory());
		}

		public FtpFileInfo[] GetFiles(string mask)
		{
			return this.FtpConnection.GetFiles(mask);
		}
	}
}
