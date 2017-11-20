using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Browsers
{
	public interface ISearchTermsReader<out TSearchTermEntry>
	{
		IEnumerable<TSearchTermEntry> GetSearchTermEntries();
	}
}
