using System;

namespace ExpertGoggles.Core.Model
{
	public interface IHistoryEntry
	{
		DateTime EntryTime { get; }
		string Url { get; }
		string Title { get; }
	}
}
