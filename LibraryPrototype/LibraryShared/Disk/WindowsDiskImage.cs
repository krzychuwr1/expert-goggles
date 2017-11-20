using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryShared.Extensions;
using LibraryShared.Interfaces.Disk;
using SleuthKit;
using SleuthKit.Structs;

namespace LibraryShared.Disk
{
    public class WindowsDiskImage : IDisk
    {
        private DiskImage diskImage;

        private List<string> filePaths = new List<string>();

        public WindowsDiskImage(DiskImage diskImage)
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

        private WalkReturnEnum FindFiles_DirectoryWalkCallback(ref TSK_FS_FILE file, string directoryPath, IntPtr dataPtr)
        {
            filePaths.Add(string.Format("{0}{1}", directoryPath, file.Name));
            return WalkReturnEnum.Continue;
        }


        public IEnumerable<string> GetAllUsers()
        {
            var users = new HashSet<string>();

            WalkReturnEnum FindUsersCallback(ref TSK_FS_FILE file, string directoryPath, IntPtr dataPtr)
            {
                if(directoryPath.StartsWith("Users") && directoryPath.Count(c => c == '/') == 2)
                {
                    users.Add(directoryPath.Split('/')[1]);
                }
                return WalkReturnEnum.Continue;
            }
            
            using (FileSystem fileSystem = diskImage.OpenFileSystem())
            {
                fileSystem.WalkDirectories(
                    FindUsersCallback,
                    DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
            }

            return users;
        }

        public Stream GetFile(string path)
        {
            using (FileSystem fileSystem = diskImage.OpenFileSystem())
            {
                var file = fileSystem.OpenFile(path);
                if (file == null)
                {
                    throw new FileNotFoundException();
                }

                var buffer = new byte[file.Size];

                file.ReadBytes(0, buffer, (int)file.Size);

                return new MemoryStream(buffer);
            }
        }

        public string GetLocalFilePath(string path)
        {
            using (var fileStream = GetFile(path))
            {
                return fileStream.SaveAsFile();
            }
        }

	    public IEnumerable<string> GetDirectoryFiles(string path)
	    {
		    using (FileSystem fileSystem = diskImage.OpenFileSystem())
		    {
			    var dir = fileSystem.OpenDirectory(path);
			    return dir.Files.Select(e => e.FileStruct.Name?.GetName());
		    }
	    }

	    public IEnumerable<string> GetDirectorySubdirectories(string path)
	    {
		    using (FileSystem fileSystem = diskImage.OpenFileSystem())
		    {
			    var dir = fileSystem.OpenDirectory(path);
			    return dir.Directories.Select(e => e.Name);
		    }
		}
    }
}
