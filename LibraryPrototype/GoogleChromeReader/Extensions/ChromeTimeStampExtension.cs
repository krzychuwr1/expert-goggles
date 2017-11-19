using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleChromeReader.Extensions
{
	public static class ChromeTimeStampExtension
	{
		public static DateTime ConvertToDateTimeFromChromeTimeStamp(this long time) => new DateTime(1601, 1, 1).AddSeconds(time / 1_000_000);
	}
}
