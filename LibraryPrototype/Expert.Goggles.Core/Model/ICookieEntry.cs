using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expert.Goggles.Core.Model
{
	public interface ICookieEntry
	{
		string Url { get; }
		string Name { get; }
	}
}
