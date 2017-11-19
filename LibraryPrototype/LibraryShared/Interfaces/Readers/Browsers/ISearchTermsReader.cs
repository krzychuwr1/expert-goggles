using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared.Interfaces.Readers.Browsers
{
	interface ISearchTermsReader<out TSearchTermEntry>
	{
		IEnumerable<TSearchTermEntry> GetSearchTermEntries();
	}
}
