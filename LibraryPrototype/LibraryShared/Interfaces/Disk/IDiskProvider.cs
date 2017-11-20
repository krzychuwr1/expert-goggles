namespace ExpertGoggles.Core.Interfaces.Disk
{
    public interface IDiskProvider
    {
        IDisk OpenDisk(string path);
    }
}
