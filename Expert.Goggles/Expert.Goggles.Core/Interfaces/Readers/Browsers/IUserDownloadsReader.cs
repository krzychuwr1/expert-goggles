using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface IUserDownloadsReader<out TDownloadEntry> where TDownloadEntry : IDownloadEntry
	{
		IEnumerable<TDownloadEntry> GetDownloadEntries(string username);
	}
}
