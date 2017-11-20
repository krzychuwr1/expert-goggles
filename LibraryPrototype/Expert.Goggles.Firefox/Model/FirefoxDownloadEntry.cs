using System;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Firefox.Model
{
	public class FirefoxDownloadEntry : IDownloadEntry
	{
		public FirefoxDownloadEntry(string url, string path, DateTime startTime)
		{
			Url = url;
			Path = path;
			StartTime = startTime;
		}

		public string Url { get; }
		public string Path { get; }
		public DateTime StartTime { get; }
	}
}
