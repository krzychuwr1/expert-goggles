using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryShared.Model;

namespace FirefoxReader.Model
{
	public class FirefoxHistoryEntry : IHistoryEntry
	{
		public FirefoxHistoryEntry(DateTime entryTime, string url, string title)
		{
			EntryTime = entryTime;
			Url = url;
			Title = title;
		}

		public DateTime EntryTime { get; }
		public string Url { get; }
		public string Title { get; }
	}
}
