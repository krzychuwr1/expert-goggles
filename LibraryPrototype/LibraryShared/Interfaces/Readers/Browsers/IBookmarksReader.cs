using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Browsers
{
	public interface IBookmarksReader<out TBookmarkEntry>
	{
		IEnumerable<TBookmarkEntry> GetBookmarkEntries();
	}
}
