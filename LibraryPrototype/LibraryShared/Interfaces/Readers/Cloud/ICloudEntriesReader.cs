using System.Collections.Generic;

namespace LibraryShared.Interfaces.Readers.Cloud
{
	public interface ICloudEntriesReader<out TEntryType>
	{
		IEnumerable<TEntryType> GetEntries();
	}
}