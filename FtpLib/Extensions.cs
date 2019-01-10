using System;

namespace FtpLib
{
	public static class Extensions
	{
		public static DateTime? ToDateTime(this WINAPI.FILETIME time)
		{
			if (time.dwHighDateTime == 0 && time.dwLowDateTime == 0)
			{
				return null;
			}
			uint dwLowDateTime = (uint)time.dwLowDateTime;
			long fileTime = (long)time.dwHighDateTime << 32 | (long)((ulong)dwLowDateTime);
			return new DateTime?(DateTime.FromFileTimeUtc(fileTime));
		}
	}
}
