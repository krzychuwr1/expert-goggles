using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryShared.Model;

namespace LibraryShared.Interfaces.Readers.Browsers
{
	public interface IDownloadsReader<out TDownloadEntry> where TDownloadEntry : IDownloadEntry
	{
		IEnumerable<TDownloadEntry> GetDownloadEntries();
	}
}
