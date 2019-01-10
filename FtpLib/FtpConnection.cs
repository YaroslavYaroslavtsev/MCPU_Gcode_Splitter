using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FtpLib
{
	public class FtpConnection : IDisposable
	{
		private IntPtr _hInternet;

		private IntPtr _hConnect;

		private string _host;

		private string _username = "";

		private string _password = "";

		private int _port = 21;

		public int Port
		{
			get
			{
				return this._port;
			}
		}

		public string Host
		{
			get
			{
				return this._host;
			}
		}

		public FtpConnection(string host)
		{
			this._host = host;
		}

		public FtpConnection(string host, int port)
		{
			this._host = host;
			this._port = port;
		}

		public FtpConnection(string host, string username, string password)
		{
			this._host = host;
			this._username = username;
			this._password = password;
		}

		public FtpConnection(string host, int port, string username, string password)
		{
			this._host = host;
			this._port = port;
			this._username = username;
			this._password = password;
		}

		public void Open()
		{
			if (string.IsNullOrEmpty(this._host))
			{
				throw new ArgumentNullException("Host");
			}
			this._hInternet = WININET.InternetOpen(Environment.UserName, 0, null, null, 4);
			if (this._hInternet == IntPtr.Zero)
			{
				this.Error();
			}
		}

		public void Login()
		{
			this.Login(this._username, this._password);
		}

		public void Login(string username, string password)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this._hConnect = WININET.InternetConnect(this._hInternet, this._host, this._port, username, password, 1, 134217728, IntPtr.Zero);
			if (this._hConnect == IntPtr.Zero)
			{
				this.Error();
			}
		}

		public void SetCurrentDirectory(string directory)
		{
			if (WININET.FtpSetCurrentDirectory(this._hConnect, directory) == 0)
			{
				this.Error();
			}
		}

		public void SetLocalDirectory(string directory)
		{
			if (Directory.Exists(directory))
			{
				Environment.CurrentDirectory = directory;
				return;
			}
			throw new InvalidDataException(string.Format("{0} is not a directory!", directory));
		}

		public string GetCurrentDirectory()
		{
			int capacity = 261;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			if (WININET.FtpGetCurrentDirectory(this._hConnect, stringBuilder, ref capacity) == 0)
			{
				this.Error();
				return null;
			}
			return stringBuilder.ToString();
		}

		public FtpDirectoryInfo GetCurrentDirectoryInfo()
		{
			string currentDirectory = this.GetCurrentDirectory();
			return new FtpDirectoryInfo(this, currentDirectory);
		}

		public long GetFileSize(string file)
		{
			IntPtr intPtr = new IntPtr(WININET.FtpOpenFile(this._hConnect, file, 2147483648u, 2, IntPtr.Zero));
			if (intPtr == IntPtr.Zero)
			{
				this.Error();
			}
			else
			{
				try
				{
					int num = 0;
					int num2 = WININET.FtpGetFileSize(intPtr, ref num);
					return (long)(num << 32 | num2);
				}
				catch (Exception)
				{
					this.Error();
				}
				finally
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return 0L;
		}

		public void GetFile(string remoteFile, bool failIfExists)
		{
			this.GetFile(remoteFile, remoteFile, failIfExists);
		}

		public void GetFile(string remoteFile, string localFile, bool failIfExists)
		{
			if (WININET.FtpGetFile(this._hConnect, remoteFile, localFile, failIfExists, 128, 2, IntPtr.Zero) == 0)
			{
				this.Error();
			}
		}

		public void PutFile(string fileName)
		{
			this.PutFile(fileName, Path.GetFileName(fileName));
		}

		public void PutFile(string localFile, string remoteFile)
		{
			if (WININET.FtpPutFile(this._hConnect, localFile, remoteFile, 2, IntPtr.Zero) == 0)
			{
				this.Error();
			}
		}

		public void RenameFile(string existingFile, string newFile)
		{
			if (WININET.FtpRenameFile(this._hConnect, existingFile, newFile) == 0)
			{
				this.Error();
			}
		}

		public void RemoveFile(string fileName)
		{
			if (WININET.FtpDeleteFile(this._hConnect, fileName) == 0)
			{
				this.Error();
			}
		}

		public void RemoveDirectory(string directory)
		{
			if (WININET.FtpRemoveDirectory(this._hConnect, directory) == 0)
			{
				this.Error();
			}
		}

		[Obsolete("Use GetFiles or GetDirectories instead.")]
		public List<string> List()
		{
			return this.List(null, false);
		}

		[Obsolete("Use GetFiles or GetDirectories instead.")]
		public List<string> List(string mask)
		{
			return this.List(mask, false);
		}

		[Obsolete("Will be removed in later releases.")]
		private List<string> List(bool onlyDirectories)
		{
			return this.List(null, onlyDirectories);
		}

		[Obsolete("Will be removed in later releases.")]
		private List<string> List(string mask, bool onlyDirectories)
		{
			WINAPI.WIN32_FIND_DATA wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
			IntPtr intPtr = WININET.FtpFindFirstFile(this._hConnect, mask, ref wIN32_FIND_DATA, 67108864, IntPtr.Zero);
			List<string> result;
			try
			{
				List<string> list = new List<string>();
				if (intPtr == IntPtr.Zero)
				{
					if (Marshal.GetLastWin32Error() == 18)
					{
						result = list;
					}
					else
					{
						this.Error();
						result = list;
					}
				}
				else
				{
					if (onlyDirectories && (wIN32_FIND_DATA.dfFileAttributes & 16) == 16)
					{
						List<string> arg_7E_0 = list;
						string arg_79_0 = new string(wIN32_FIND_DATA.fileName);
						char[] trimChars = new char[1];
						arg_7E_0.Add(arg_79_0.TrimEnd(trimChars));
					}
					else if (!onlyDirectories)
					{
						List<string> arg_A4_0 = list;
						string arg_9F_0 = new string(wIN32_FIND_DATA.fileName);
						char[] trimChars2 = new char[1];
						arg_A4_0.Add(arg_9F_0.TrimEnd(trimChars2));
					}
					wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					while (WININET.InternetFindNextFile(intPtr, ref wIN32_FIND_DATA) != 0)
					{
						if (onlyDirectories && (wIN32_FIND_DATA.dfFileAttributes & 16) == 16)
						{
							List<string> arg_E0_0 = list;
							string arg_DB_0 = new string(wIN32_FIND_DATA.fileName);
							char[] trimChars3 = new char[1];
							arg_E0_0.Add(arg_DB_0.TrimEnd(trimChars3));
						}
						else if (!onlyDirectories)
						{
							List<string> arg_106_0 = list;
							string arg_101_0 = new string(wIN32_FIND_DATA.fileName);
							char[] trimChars4 = new char[1];
							arg_106_0.Add(arg_101_0.TrimEnd(trimChars4));
						}
						wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					}
					if (Marshal.GetLastWin32Error() != 18)
					{
						this.Error();
					}
					result = list;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return result;
		}

		public FtpFileInfo[] GetFiles()
		{
			return this.GetFiles(this.GetCurrentDirectory());
		}

		public FtpFileInfo[] GetFiles(string mask)
		{
			WINAPI.WIN32_FIND_DATA wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
			IntPtr intPtr = WININET.FtpFindFirstFile(this._hConnect, mask, ref wIN32_FIND_DATA, 67108864, IntPtr.Zero);
			FtpFileInfo[] result;
			try
			{
				List<FtpFileInfo> list = new List<FtpFileInfo>();
				if (intPtr == IntPtr.Zero)
				{
					if (Marshal.GetLastWin32Error() == 18)
					{
						result = list.ToArray();
					}
					else
					{
						this.Error();
						result = list.ToArray();
					}
				}
				else
				{
					if ((wIN32_FIND_DATA.dfFileAttributes & 16) != 16)
					{
						string arg_82_0 = new string(wIN32_FIND_DATA.fileName);
						char[] trimChars = new char[1];
						list.Add(new FtpFileInfo(this, arg_82_0.TrimEnd(trimChars))
						{
							LastAccessTime = wIN32_FIND_DATA.ftLastAccessTime.ToDateTime(),
							LastWriteTime = wIN32_FIND_DATA.ftLastWriteTime.ToDateTime(),
							CreationTime = wIN32_FIND_DATA.ftCreationTime.ToDateTime(),
							Attributes = (FileAttributes)wIN32_FIND_DATA.dfFileAttributes
						});
					}
					wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					while (WININET.InternetFindNextFile(intPtr, ref wIN32_FIND_DATA) != 0)
					{
						if ((wIN32_FIND_DATA.dfFileAttributes & 16) != 16)
						{
							string arg_109_0 = new string(wIN32_FIND_DATA.fileName);
							char[] trimChars2 = new char[1];
							list.Add(new FtpFileInfo(this, arg_109_0.TrimEnd(trimChars2))
							{
								LastAccessTime = wIN32_FIND_DATA.ftLastAccessTime.ToDateTime(),
								LastWriteTime = wIN32_FIND_DATA.ftLastWriteTime.ToDateTime(),
								CreationTime = wIN32_FIND_DATA.ftCreationTime.ToDateTime(),
								Attributes = (FileAttributes)wIN32_FIND_DATA.dfFileAttributes
							});
						}
						wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					}
					if (Marshal.GetLastWin32Error() != 18)
					{
						this.Error();
					}
					result = list.ToArray();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return result;
		}

		public FtpDirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories(this.GetCurrentDirectory());
		}

		public FtpDirectoryInfo[] GetDirectories(string path)
		{
			WINAPI.WIN32_FIND_DATA wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
			IntPtr intPtr = WININET.FtpFindFirstFile(this._hConnect, path, ref wIN32_FIND_DATA, 67108864, IntPtr.Zero);
			FtpDirectoryInfo[] result;
			try
			{
				List<FtpDirectoryInfo> list = new List<FtpDirectoryInfo>();
				if (intPtr == IntPtr.Zero)
				{
					if (Marshal.GetLastWin32Error() == 18)
					{
						result = list.ToArray();
					}
					else
					{
						this.Error();
						result = list.ToArray();
					}
				}
				else
				{
					if ((wIN32_FIND_DATA.dfFileAttributes & 16) == 16)
					{
						string arg_82_0 = new string(wIN32_FIND_DATA.fileName);
						char[] trimChars = new char[1];
						list.Add(new FtpDirectoryInfo(this, arg_82_0.TrimEnd(trimChars))
						{
							LastAccessTime = wIN32_FIND_DATA.ftLastAccessTime.ToDateTime(),
							LastWriteTime = wIN32_FIND_DATA.ftLastWriteTime.ToDateTime(),
							CreationTime = wIN32_FIND_DATA.ftCreationTime.ToDateTime(),
							Attributes = (FileAttributes)wIN32_FIND_DATA.dfFileAttributes
						});
					}
					wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					while (WININET.InternetFindNextFile(intPtr, ref wIN32_FIND_DATA) != 0)
					{
						if ((wIN32_FIND_DATA.dfFileAttributes & 16) == 16)
						{
							string arg_109_0 = new string(wIN32_FIND_DATA.fileName);
							char[] trimChars2 = new char[1];
							list.Add(new FtpDirectoryInfo(this, arg_109_0.TrimEnd(trimChars2))
							{
								LastAccessTime = wIN32_FIND_DATA.ftLastAccessTime.ToDateTime(),
								LastWriteTime = wIN32_FIND_DATA.ftLastWriteTime.ToDateTime(),
								CreationTime = wIN32_FIND_DATA.ftCreationTime.ToDateTime(),
								Attributes = (FileAttributes)wIN32_FIND_DATA.dfFileAttributes
							});
						}
						wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
					}
					if (Marshal.GetLastWin32Error() != 18)
					{
						this.Error();
					}
					result = list.ToArray();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return result;
		}

		public void CreateDirectory(string path)
		{
			if (WININET.FtpCreateDirectory(this._hConnect, path) == 0)
			{
				this.Error();
			}
		}

		public bool DirectoryExists(string path)
		{
			WINAPI.WIN32_FIND_DATA wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
			IntPtr intPtr = WININET.FtpFindFirstFile(this._hConnect, path, ref wIN32_FIND_DATA, 67108864, IntPtr.Zero);
			bool result;
			try
			{
				if (intPtr == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return result;
		}

		public bool FileExists(string path)
		{
			WINAPI.WIN32_FIND_DATA wIN32_FIND_DATA = default(WINAPI.WIN32_FIND_DATA);
			IntPtr intPtr = WININET.FtpFindFirstFile(this._hConnect, path, ref wIN32_FIND_DATA, 67108864, IntPtr.Zero);
			bool result;
			try
			{
				if (intPtr == IntPtr.Zero)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					WININET.InternetCloseHandle(intPtr);
				}
			}
			return result;
		}

		public string SendCommand(string cmd)
		{
			IntPtr intPtr = IntPtr.Zero;
			int num;
			if (cmd != null && cmd == "PASV")
			{
				num = WININET.FtpCommand(this._hConnect, false, 1, cmd, IntPtr.Zero, ref intPtr);
			}
			else
			{
				num = WININET.FtpCommand(this._hConnect, false, 1, cmd, IntPtr.Zero, ref intPtr);
			}
			int num2 = 8192;
			if (num == 0)
			{
				this.Error();
			}
			else if (intPtr != IntPtr.Zero)
			{
				StringBuilder stringBuilder = new StringBuilder(num2);
				int num3 = 0;
				do
				{
					num = WININET.InternetReadFile(intPtr, stringBuilder, num2, ref num3);
				}
				while (num == 1 && num3 > 1);
				return stringBuilder.ToString();
			}
			return "";
		}

		public void Close()
		{
			WININET.InternetCloseHandle(this._hConnect);
			this._hConnect = IntPtr.Zero;
			WININET.InternetCloseHandle(this._hInternet);
			this._hInternet = IntPtr.Zero;
		}

		private string InternetLastResponseInfo(ref int code)
		{
			int capacity = 8192;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			WININET.InternetGetLastResponseInfo(ref code, stringBuilder, ref capacity);
			return stringBuilder.ToString();
		}

		private void Error()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 12003)
			{
				string message = this.InternetLastResponseInfo(ref lastWin32Error);
				throw new FtpException(lastWin32Error, message);
			}
			throw new Win32Exception(lastWin32Error);
		}

		public void Dispose()
		{
			if (this._hConnect != IntPtr.Zero)
			{
				WININET.InternetCloseHandle(this._hConnect);
			}
			if (this._hInternet != IntPtr.Zero)
			{
				WININET.InternetCloseHandle(this._hInternet);
			}
		}
	}
}
