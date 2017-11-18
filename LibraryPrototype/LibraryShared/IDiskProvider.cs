namespace LibraryShared
{
    public interface IDiskProvider
    {
        IDisk OpenDisk(string path);
    }
}
