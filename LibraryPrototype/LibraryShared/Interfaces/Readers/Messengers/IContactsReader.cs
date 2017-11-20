using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Messengers
{
    public interface IContactsReader<out TContactEntry>
    {
        IEnumerable<TContactEntry> GetContactEntries(string appUserName);
    }
}
