using LibraryShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDrive
{
    public interface IGoogleDriveReader
    {
        string GetAllDriveData();
    }

    public class GoogleDriveReader : IGoogleDriveReader
    {
        private readonly IFileProvider fileProvider;

        public GoogleDriveReader(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public string GetAllDriveData() => throw new NotImplementedException();
    }
}
