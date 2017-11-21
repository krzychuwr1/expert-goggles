using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Chrome.Model
{
	public class ChromeSearchTermEntry : ISearchTermEntry
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
