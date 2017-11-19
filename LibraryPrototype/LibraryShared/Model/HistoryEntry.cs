using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared.Model
{
	public interface IHistoryEntry
	{
		DateTime EntryTime { get; }
		string Url { get; }
		string Title { get; }
	}
}
