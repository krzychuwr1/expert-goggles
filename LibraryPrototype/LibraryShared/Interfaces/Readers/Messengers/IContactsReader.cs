using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared.Interfaces.Readers.Messengers
{
    public interface IContactsReader<out TContactEntry>
    {
        IEnumerable<TContactEntry> GetContactEntries();
    }
}
