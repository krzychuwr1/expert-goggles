using System.Collections.Generic;

namespace LibraryShared.Interfaces.Readers
{
    public interface IMetadataReader<out TMetadataType> 
    {
        TMetadataType GetMetadata();
    }
}