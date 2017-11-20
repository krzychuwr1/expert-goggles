using System.Collections.Generic;

namespace ExpertGoggles.Core.Interfaces.Readers.Messengers
{
    public interface ITextMessegesReader<out TMessegesEntry>
    {
        IEnumerable<TMessegesEntry> GetMessagesEntries(string appUserName);
    }
}
