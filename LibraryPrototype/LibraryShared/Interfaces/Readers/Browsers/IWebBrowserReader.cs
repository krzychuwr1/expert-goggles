using System.Collections.Generic;
using ExpertGoggles.Core.Model;

namespace ExpertGoggles.Core.Interfaces.Readers.Browsers
{
	public interface IBrowsingHistoryReader<out THistoryEntry> where THistoryEntry : IHistoryEntry
	{
		IEnumerable<THistoryEntry> GetHistoryEntries();
	}
}
