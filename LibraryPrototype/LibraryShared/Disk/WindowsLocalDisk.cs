using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExpertGoggles.Core.Interfaces.Disk;

namespace ExpertGoggles.Core.Disk
{
	public class WindowsLocalDisk : IDisk
	{
		public IEnumerable<string> GetAllFilePaths()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetAllUsers()
		{
			yield return Environment.UserName;
			//DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
			//DirectoryEntry admGroup = localMachine.Children.Find("users", "group");
			//object members = admGroup.Invoke("members", null);
			//foreach (object groupMember in (IEnumerable)members)
			//{
			//	DirectoryEntry member = new DirectoryEntry(groupMember);
			//	yield return member.Name;
			//}
		}

		public Stream GetFile(string path) => File.OpenRead(AddLocalDiskPrefix(path));

		public string GetLocalFilePath(string path) => AddLocalDiskPrefix(path);
		public IEnumerable<string> GetDirectoryFiles(string path) => new DirectoryInfo(AddLocalDiskPrefix(path)).GetFiles().Select(f => f.Name);
		public IEnumerable<string> GetDirectorySubdirectories(string path) => new DirectoryInfo(AddLocalDiskPrefix(path)).GetDirectories().Select(f => f.Name);

		private string AddLocalDiskPrefix(string path) => path.StartsWith(@"C:\") ? path : $@"C:\{path}";
	}
}