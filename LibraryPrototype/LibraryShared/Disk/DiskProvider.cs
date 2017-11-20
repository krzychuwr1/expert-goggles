using System.IO;
using Expert.Goggles.Core.Interfaces.Disk;
using SleuthKit;

namespace Expert.Goggles.Core.Disk
{
    public class DiskProvider : IDiskProvider
    {
        public IDisk OpenDisk(string path)
        {
            var file = new FileInfo(path);
            var diskImage = new DiskImage(file);
            return new WindowsDiskImage(diskImage);
        }

	    public IDisk OpenDisk()
	    {
		    return new WindowsLocalDisk();
	    }
    }
}
