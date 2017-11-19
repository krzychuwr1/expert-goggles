namespace LibraryShared.Interfaces.Disk
{
    public interface IDiskProvider
    {
        IDisk OpenDisk(string path);
    }
}
