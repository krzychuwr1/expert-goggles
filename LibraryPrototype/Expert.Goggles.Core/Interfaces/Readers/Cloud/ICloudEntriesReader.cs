using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Cloud
{
	public interface ICloudEntriesReader<out TEntryType> where TEntryType : IFileActionEntry
	{
		IEnumerable<TEntryType> GetEntries();
	}
}