namespace LibraryShared
{
    public interface IFileProvider
    {
        FileProviderDisk OpenDisk(string path);
    }
}
