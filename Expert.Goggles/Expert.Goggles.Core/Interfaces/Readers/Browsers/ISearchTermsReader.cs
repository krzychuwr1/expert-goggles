using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface ISearchTermsReader<out TSearchTermEntry> where TSearchTermEntry : ISearchTermEntry
	{
		IEnumerable<TSearchTermEntry> GetSearchTermEntries();
	}
}
