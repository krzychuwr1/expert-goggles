using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Firefox.Model
{
	public class FirefoxMetadata : IMetadata
	{
		public FirefoxMetadata(IEnumerable<string> users)
		{
			Users = users;
		}

		public IEnumerable<string> Users { get; }
	}
}
