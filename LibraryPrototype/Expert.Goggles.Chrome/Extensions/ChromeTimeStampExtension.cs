using System;

namespace Expert.Goggles.Chrome.Extensions
{
	public static class ChromeTimeStampExtension
	{
		public static DateTime ConvertToDateTimeFromChromeTimeStamp(this long time) => new DateTime(1601, 1, 1).AddSeconds(time / 1_000_000);
	}
}
