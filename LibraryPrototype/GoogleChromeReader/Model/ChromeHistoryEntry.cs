using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleChromeReader.Model
{
	public class ChromeHistoryEntry
	{
		public DateTime EntryTime { get; }
		public string Url { get; }
		public string Title { get; }

		public ChromeHistoryEntry(DateTime entryTime, string url, string title)
		{
			EntryTime = entryTime;
			Url = url;
			Title = title;
		}
	}
}
