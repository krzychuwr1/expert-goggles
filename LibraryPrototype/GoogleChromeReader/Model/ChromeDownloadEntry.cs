using System;
using LibraryShared.Model;

namespace GoogleChromeReader.Model
{
	public class ChromeDownloadEntry : IDownloadEntry
	{
		public string Url { get; }
		public string Path { get; }
		public DateTime StartTime { get; }
		public DateTime EndTime { get; }
		public long DownloadedSizeKb { get; }
		public long TotalSizeKb { get; }
		public EChromeDownloadState State { get; }

		public ChromeDownloadEntry(string url, string path, DateTime startTime, DateTime endTime, long downloadedSizeKb, long totalSizeKb, EChromeDownloadState state)
		{
			Url = url;
			Path = path;
			StartTime = startTime;
			EndTime = endTime;
			DownloadedSizeKb = downloadedSizeKb;
			TotalSizeKb = totalSizeKb;
			State = state;
		}
	}
}
