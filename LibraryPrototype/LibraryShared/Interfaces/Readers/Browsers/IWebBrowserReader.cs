using System.Collections;
using System.Collections.Generic;

namespace LibraryShared.Interfaces.Readers.Browsers
{
	public interface IBrowsingHistoryReader<out THistoryEntry>
	{
		IEnumerable<THistoryEntry> GetHistoryEntries();
	}
}
