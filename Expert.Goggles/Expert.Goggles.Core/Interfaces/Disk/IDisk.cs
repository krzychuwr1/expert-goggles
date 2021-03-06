using System.Collections.Generic;
using System.IO;

namespace Expert.Goggles.Core.Interfaces.Disk
{
	public interface IDisk
	{
		IEnumerable<string> GetAllFilePaths();
		IEnumerable<string> GetAllUsers();
		Stream GetFile(string path);
        string GetLocalFilePath(string path);
		IEnumerable<string> GetDirectoryFiles(string path);
		IEnumerable<string> GetDirectorySubdirectories(string path);
		bool CheckIfDirectoryExists(string path);
	}
}