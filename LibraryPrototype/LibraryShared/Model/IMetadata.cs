using System.Collections.Generic;

namespace Expert.Goggles.Core.Model
{
	public interface IMetadata
	{
		IEnumerable<string> Users { get; }
	}
}