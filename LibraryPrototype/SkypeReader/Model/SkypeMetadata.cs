using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.Skype.Model
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