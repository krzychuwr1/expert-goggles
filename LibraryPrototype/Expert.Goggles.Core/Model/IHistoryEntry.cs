using System;

namespace Expert.Goggles.Core.Model
{
	public interface IHistoryEntry
	{
		DateTime EntryTime { get; }
		string Url { get; }
		string Title { get; }
	}
}
