using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Browsers
{
	public interface ICookiesReader<out TCookieEntry>
	{
		IEnumerable<TCookieEntry> GetCookies();
	}
}
