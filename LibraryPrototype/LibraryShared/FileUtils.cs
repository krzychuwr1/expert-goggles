using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShared
{
    public static class FileUtils
    {
        public static string SaveAsFile(this Stream stream)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\tmp";
            Directory.CreateDirectory(path);
            var filePath = path +  "\\" +DateTime.Now.Ticks;

            using (var fileStream = File.Create(filePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            return filePath;
        }
    }
}
