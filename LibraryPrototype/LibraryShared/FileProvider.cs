using SleuthKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared
{
    public class FileProvider : IFileProvider
    {
        public FileProviderDisk OpenDisk(string path)
        {
            var file = new FileInfo(path);
            var diskImage = new DiskImage(file);
            return new FileProviderDisk(diskImage);
        }
    }
}
