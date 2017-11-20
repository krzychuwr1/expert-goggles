using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared.Interfaces.Readers.Messengers
{
    public interface ICallsReader<out TCallEntry>
    {
        IEnumerable<TCallEntry> GetCallEntries(string appUsername);
    }
}
