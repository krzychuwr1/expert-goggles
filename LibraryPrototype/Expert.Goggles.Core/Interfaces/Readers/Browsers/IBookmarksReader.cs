using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IBookmarksReader<out TBookmarkEntry> where TBookmarkEntry : IBookmarkEntry
	{
		IEnumerable<TBookmarkEntry> GetBookmarkEntries();
	}
}
