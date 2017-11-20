using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Cloud
{
	public interface ICloudEntriesReader<out TEntryType>
	{
		IEnumerable<TEntryType> GetEntries();
	}
}