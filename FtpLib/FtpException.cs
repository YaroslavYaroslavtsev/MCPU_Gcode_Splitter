using System;

namespace FtpLib
{
	public class FtpException : Exception
	{
		private int _error;

		public int ErrorCode
		{
			get
			{
				return this._error;
			}
		}

		public FtpException(int error, string message) : base(message)
		{
			this._error = error;
		}
	}
}
