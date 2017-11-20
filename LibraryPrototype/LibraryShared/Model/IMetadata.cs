using System.Collections.Generic;

namespace ExpertGoggles.Core.Model
{
	public interface IMetadata
	{
		IEnumerable<string> Users { get; }
	}
}