using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleuthKit;
using SleuthKit.Structs;

namespace LibraryShared
{
    public class FileProviderDisk
    {
        private DiskImage diskImage;

        private List<string> filePaths = new List<string>();

        public FileProviderDisk(DiskImage diskImage)
        {
            this.diskImage = diskImage;
        }

        public IEnumerable<string> GetAllFilePaths()
        {
            if(!filePaths.Any())
            {
                using (FileSystem fileSystem = diskImage.OpenFileSystem())
                {
                    fileSystem.WalkDirectories(
                        FindFiles_DirectoryWalkCallback,
                        DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
                }
            }

            return filePaths;
        }

        private WalkReturnEnum FindFiles_DirectoryWalkCallback(
        ref TSK_FS_FILE file,
        string directoryPath,
        IntPtr dataPtr)
        {
            filePaths.Add(string.Format("{0}{1}", directoryPath, file.Name));
            return WalkReturnEnum.Continue;
        }
    }
}
