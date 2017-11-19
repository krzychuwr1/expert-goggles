using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleChromeReader.Model;
using LibraryShared.Interfaces.Disk;
using LibraryShared.Interfaces.Readers.Browsers;

namespace GoogleChromeReader
{
	public interface IGoogleChromeReader : IBrowsingHistoryReader<ChromeHistoryEntry>
	{
	}

	public class GoogleDriveReader : IGoogleChromeReader
	{
		private readonly IDisk _disk;
		private readonly string _userName;

		public GoogleDriveReader(IDisk disk, string userName)
		{
			_disk = disk;
			_userName = userName;
		}

		public IEnumerable<ChromeHistoryEntry> GetHistoryEntries()
		{
			throw new NotImplementedException();
		}
	}
}
