using System.Collections.Generic;
using ExpertGoggles.Core.Model;

namespace GoogleDriveReader
{
    public class GoogleDriveMetadata : IMetadata
    {
        public string UserEmail { get; set; }

        public string AppVersion { get; set; }

        private List<string> filenames = new List<string>();
	    public IEnumerable<string> Users { get; }
    }
}
