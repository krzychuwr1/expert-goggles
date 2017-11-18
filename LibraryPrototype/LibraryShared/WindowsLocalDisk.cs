using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;

namespace LibraryShared
{
	public class WindowsLocalDisk : IDisk
	{
		public IEnumerable<string> GetAllFilePaths()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetAllUsers()
		{
			DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
			DirectoryEntry admGroup = localMachine.Children.Find("users", "group");
			object members = admGroup.Invoke("members", null);
			foreach (object groupMember in (IEnumerable)members)
			{
				DirectoryEntry member = new DirectoryEntry(groupMember);
				yield return member.Name;
			}
		}

		public Stream GetFile(string path)
		{
            return File.OpenRead(path);
		}

        public string GetLocalFilePath(string path)
        {
            return path;
        }
    }
}