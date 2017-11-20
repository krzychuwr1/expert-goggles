using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Messengers
{
    public interface ICallsReader<out TCallEntry>
    {
        IEnumerable<TCallEntry> GetCallEntries(string appUsername);
    }
}
