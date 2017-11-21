using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Core.Interfaces.Readers
{
    public interface IMetadataReader<out TMetadataType> where TMetadataType : IMetadata 
    {
        TMetadataType GetMetadata();
    }
}