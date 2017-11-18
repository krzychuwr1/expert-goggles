using SleuthKit;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared
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
