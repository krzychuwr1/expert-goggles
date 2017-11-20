using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Cloud
{
	public interface ICloudEntriesReader<out TEntryType>
	{
		IEnumerable<TEntryType> GetEntries();
	}
}