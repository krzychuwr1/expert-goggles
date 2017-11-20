using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IBrowsingHistoryReader<out THistoryEntry> where THistoryEntry : IHistoryEntry
	{
		IEnumerable<THistoryEntry> GetHistoryEntries();
	}
}
