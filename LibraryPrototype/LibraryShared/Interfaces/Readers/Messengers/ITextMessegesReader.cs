using System.Collections.Generic;

namespace Expert.Goggles.Core.Interfaces.Readers.Messengers
{
    public interface ITextMessegesReader<out TMessegesEntry>
    {
        IEnumerable<TMessegesEntry> GetMessagesEntries(string appUserName);
    }
}
