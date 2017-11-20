using System;
using ExpertGoggles.Core.Model;

namespace ExpertGoggles.Firefox.Model
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
