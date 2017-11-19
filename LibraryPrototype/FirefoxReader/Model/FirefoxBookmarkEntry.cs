using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirefoxReader.Model
{
	public class FirefoxBookmarkEntry
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
