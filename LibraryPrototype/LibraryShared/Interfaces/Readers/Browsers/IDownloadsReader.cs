using System.Collections.Generic;
using ExpertGoggles.Core.Model;

namespace ExpertGoggles.Core.Interfaces.Readers.Browsers
{
	public interface IDownloadsReader<out TDownloadEntry> where TDownloadEntry : IDownloadEntry
	{
		IEnumerable<TDownloadEntry> GetDownloadEntries();
	}
}
