using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Firefox.Model
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
