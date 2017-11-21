using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Chrome.Model
{
	public class ChromeHistoryEntry : IHistoryEntry
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
