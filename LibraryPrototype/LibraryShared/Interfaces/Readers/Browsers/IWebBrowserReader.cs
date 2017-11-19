using System.Collections;
using System.Collections.Generic;
using LibraryShared.Model;

namespace LibraryShared.Interfaces.Readers.Browsers
{
	public interface IBrowsingHistoryReader<out THistoryEntry> where THistoryEntry : IHistoryEntry
	{
		IEnumerable<THistoryEntry> GetHistoryEntries();
	}
}
