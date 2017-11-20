using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Browsers
{
	public interface ICookiesReader<out TCookieEntry>
	{
		IEnumerable<TCookieEntry> GetCookies();
	}
}
