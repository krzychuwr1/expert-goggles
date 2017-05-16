using SleuthKit;
using SleuthKit.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directory = SleuthKit.Directory;
using File = SleuthKit.File;

namespace Tester
{
    class Program
    {
        private static readonly IList<string> FilePaths = new List<string>();
        private static int allFileCount;

        /// <summary>
        ///     The file count.
        /// </summary>
        private static int jpgFileCount;
        static void Main(string[] args)
        {
            var file = new FileInfo("..//..//resources/USB-disk-image-FAT.E01");
            var diskImage = new DiskImage(file);
            int volumeAdress = 2;

            using (VolumeSystem volumeSystem = diskImage.OpenVolumeSystem())
            {
                Volume volume = volumeSystem.Volumes.SingleOrDefault(v => v.Address == volumeAdress);

                //Assert.NotNull(volume);

                using (FileSystem fileSystem = volume.OpenFileSystem())
                {
                    //count += CountFiles(fileSystem.OpenRootDirectory());
                    fileSystem.WalkDirectories(
                        FindFiles_DirectoryWalkCallback,
                        DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
                }
                Console.ReadKey();
                //Assert.AreEqual(37, FilePaths.Count()); //I think it should be 63 || Bala: Autopsy shows me that there are only 30 files
            }


        }

        private static WalkReturnEnum FileCount_DirectoryWalkCallback(
            ref TSK_FS_FILE file,
            string directoryPath,
            IntPtr dataPtr)
        {
            if (file.Name.ToString().Contains("jpg"))
            {
                jpgFileCount++;
            }
            allFileCount++;
            return WalkReturnEnum.Continue;
        }

        /// <summary>
        ///     Callback function that is called for each file name during directory walk. (FileSystem.WalkDirectories)
        /// </summary>
        /// <param name="file">
        ///     The file struct.
        /// </param>
        /// <param name="directoryPath">
        ///     The directory path.
        /// </param>
        /// <param name="dataPtr">
        ///     Pointer to data that is passed to the callback function each time.
        /// </param>
        /// <returns>
        ///     Value to control the directory walk.
        /// </returns>
        private static WalkReturnEnum FindFiles_DirectoryWalkCallback(
            ref TSK_FS_FILE file,
            string directoryPath,
            IntPtr dataPtr)
        {
            FilePaths.Add(string.Format("{0}{1}", directoryPath, file.Name));
            return WalkReturnEnum.Continue;
        }

        private int CountFiles(SleuthKit.Directory directory)
        {
            int count = 0;

            if (directory == null)
            {
                return 0;
            }

            foreach (Directory subDirectory in directory.Directories)
            {
                count += this.CountFiles(subDirectory);
            }

            count += directory.Files.Count();

            return count;
        }

        private IEnumerable<String> FindFiles(Directory directory)
        {
            if (directory != null)
            {
                foreach (Directory subDirectory in directory.Directories)
                {
                    foreach (String path in this.FindFiles(subDirectory))
                    {
                        yield return path;
                    }
                }

                foreach (File file in directory.Files)
                {
                    yield return file.Path;
                }
            }
        }
    }
}
