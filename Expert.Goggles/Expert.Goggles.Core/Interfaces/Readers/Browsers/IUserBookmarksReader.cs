using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IUserBookmarksReader<out TBookmarkEntry> where TBookmarkEntry : IBookmarkEntry
	{
		IEnumerable<TBookmarkEntry> GetBookmarkEntries(string username);
	}
}
