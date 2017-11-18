using GoogleDrive;
using LibraryShared;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleDriveReader;

namespace LibraryPrototype
{
    public static class NinjectConfig
    {
        public static void ConfigureBindings(this IKernel kernel)
        {
            kernel.Bind<IGoogleDriveReader>().To<GoogleDriveReader.GoogleDriveReader>();
            kernel.Bind<IFileProvider>().To<FileProvider>();
        }

    }
}
