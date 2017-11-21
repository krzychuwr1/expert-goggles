using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface ICookiesReader<out TCookieEntry> where TCookieEntry : ICookieEntry
	{
		IEnumerable<TCookieEntry> GetCookies();
	}
}
