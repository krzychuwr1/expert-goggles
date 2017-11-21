using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Messengers
{
    public interface IContactsReader<out TContactEntry>
    {
        IEnumerable<TContactEntry> GetContactEntries(string appUserName);
    }
}
