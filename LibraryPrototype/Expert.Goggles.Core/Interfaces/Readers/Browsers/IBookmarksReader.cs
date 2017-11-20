using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IBookmarksReader<out TBookmarkEntry>
	{
		IEnumerable<TBookmarkEntry> GetBookmarkEntries();
	}
}
