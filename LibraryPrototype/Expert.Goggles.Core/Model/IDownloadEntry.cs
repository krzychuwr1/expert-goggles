using System;

namespace Expert.Goggles.Core.Model
{
	public interface IDownloadEntry
	{
		string Url { get; }
		string Path { get; }
		DateTime StartTime { get; }
	}
}
