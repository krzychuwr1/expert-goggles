using ExpertGoggles.Core.Model;

namespace ExpertGoggles.Core.Interfaces.Readers
{
    public interface IMetadataReader<out TMetadataType> where TMetadataType : IMetadata 
    {
        TMetadataType GetMetadata();
    }
}