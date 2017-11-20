using System.Collections.Generic;
using LibraryShared.Model;

namespace SkypeReader.Model
{
    public class SkypeMetadata : IMetadata
    {
		public SkypeMetadata(string path, IEnumerable<string> users)
		{
			Path = path;
			Users = users;
		}

		public string Path { get; set; }
	    public IEnumerable<string> Users { get; }
    }
}