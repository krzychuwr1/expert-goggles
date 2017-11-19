using System;

namespace GoogleChromeReader
{
	public class ChromeSearchTermEntry
	{
		public string Term { get; }
		public DateTime LastSearchTime { get; }
		public long Count { get; }

		public ChromeSearchTermEntry(string term, DateTime lastSearchTime, long count)
		{
			Term = term;
			LastSearchTime = lastSearchTime;
			Count = count;
		}
	}
}
