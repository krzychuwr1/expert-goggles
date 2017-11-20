using System.IO;
using ExpertGoggles.Core.Interfaces.Disk;
using SleuthKit;

namespace ExpertGoggles.Core.Disk
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
