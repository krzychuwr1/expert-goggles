using System.Collections.Generic;

namespace GoogleDrive
{
    public class GoogleDriveMetadata
    {
        public string UserEmail { get; set; }

        public string AppVersion { get; set; }

        private List<string> filenames = new List<string>();
    }
}
