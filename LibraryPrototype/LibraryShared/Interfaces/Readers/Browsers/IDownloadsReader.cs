using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IDownloadsReader<out TDownloadEntry> where TDownloadEntry : IDownloadEntry
	{
		IEnumerable<TDownloadEntry> GetDownloadEntries();
	}
}
