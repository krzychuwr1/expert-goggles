using GoogleDrive;
using LibraryShared;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleDriveReader;
using LibraryShared.Interfaces.Disk;
using Ninject.Parameters;

namespace LibraryPrototype
{
    public class LibraryMain
    {
        private static readonly IKernel kernel = new StandardKernel();
        
        private readonly IGoogleDriveReader googleDriveReader;

        public static IResolutionRoot Kernel { get => kernel as IResolutionRoot; }

        public LibraryMain()
        {
            kernel.ConfigureBindings();
        }

        public IGoogleDriveReader GetGoogleDriveReader(IDisk disk, string userName) => new GoogleDriveReader.GoogleDriveReader(disk, userName);
         
    }
}
