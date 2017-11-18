using System.Collections;
using System.Collections.Generic;

namespace LibraryShared
{
    public interface IReader<T> 
    {
        IEnumerable<T> GetData();
    }
}