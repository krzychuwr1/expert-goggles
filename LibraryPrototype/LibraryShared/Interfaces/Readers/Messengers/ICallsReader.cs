using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Messengers
{
    public interface ICallsReader<out TCallEntry>
    {
        IEnumerable<TCallEntry> GetCallEntries(string appUsername);
    }
}
