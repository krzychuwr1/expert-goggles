using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared.Model
{
	public interface IDownloadEntry
	{
		string Url { get; }
		string Path { get; }
		DateTime StartTime { get; }
	}
}
