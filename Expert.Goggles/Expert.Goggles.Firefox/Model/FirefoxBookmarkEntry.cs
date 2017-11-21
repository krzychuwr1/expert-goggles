using System;
using Expert.Goggles.Core.Interfaces.Readers.Browsers;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Firefox.Model
{
	public class FirefoxBookmarkEntry : IBookmarkEntry
	{
		public string Url { get; }
		public string Title { get; }
		public DateTime? LastVisited { get; }
		public DateTime LastModified { get; }
		public long VisitCount { get; }

		public FirefoxBookmarkEntry(string url, string title, DateTime? lastVisited, DateTime lastModified, long visitCount)
		{
			Url = url;
			Title = title;
			LastVisited = lastVisited;
			LastModified = lastModified;
			VisitCount = visitCount;
		}
	}
}
