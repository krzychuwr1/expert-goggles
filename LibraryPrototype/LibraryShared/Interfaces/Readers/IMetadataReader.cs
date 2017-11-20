using System.Collections.Generic;
using LibraryShared.Model;

namespace LibraryShared.Interfaces.Readers
{
    public interface IMetadataReader<out TMetadataType> where TMetadataType : IMetadata 
    {
        TMetadataType GetMetadata();
    }
}