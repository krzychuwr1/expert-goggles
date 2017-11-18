using System.Collections;
using System.Collections.Generic;

namespace LibraryShared
{
    public interface IReader<EntryType,MetadataType> 
    {
        IEnumerable<EntryType> GetData();
        MetadataType GetMetadata();
    }
}