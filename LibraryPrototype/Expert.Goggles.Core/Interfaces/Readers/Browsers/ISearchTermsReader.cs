using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface ISearchTermsReader<out TSearchTermEntry>
	{
		IEnumerable<TSearchTermEntry> GetSearchTermEntries();
	}
}
