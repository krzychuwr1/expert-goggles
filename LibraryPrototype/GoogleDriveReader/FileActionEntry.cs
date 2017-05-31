using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDrive
{
    public enum Direction
    {
        DOWNLOAD,
        UPLOAD,
        INTERNAL
    }

    public enum Action
    {
        CREATE,
        DELETE,
        MODIFY,
        MOVE,
        RENAME,
        CHANGE_ACL
    }

    public class FileActionEntry
    {
        public DateTime? Date { get; set; }

        public Direction? Direction { get; set; }

        public Action? Action { get; set; }

        public string FileName { get; set; }

        public long? FileSize { get; set; }

        public string Path { get; set; }
    }
}
