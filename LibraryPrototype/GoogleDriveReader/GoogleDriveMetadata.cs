using System.Collections.Generic;
using Expert.Goggles.Core.Model;

namespace Expert.Goggles.GoogleDrive
{
    public class GoogleDriveMetadata : IMetadata
    {
        public string UserEmail { get; set; }

        public string AppVersion { get; set; }

        private List<string> filenames = new List<string>();
	    public IEnumerable<string> Users { get; }
    }
}
