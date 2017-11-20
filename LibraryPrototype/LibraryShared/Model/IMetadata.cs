using System.Collections.Generic;

namespace LibraryShared.Model
{
	public interface IMetadata
	{
		IEnumerable<string> Users { get; }
	}
}