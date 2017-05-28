using GoogleDrive;
using LibraryShared;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPrototype
{
    public class Main
    {
        private static readonly IKernel kernel = new StandardKernel();
        
        private readonly IGoogleDriveReader googleDriveReader;

        public static IResolutionRoot Kernel { get => kernel as IResolutionRoot; }

        public Main()
        {
            kernel.ConfigureBindings();
            googleDriveReader = Kernel.Get<IGoogleDriveReader>();   
        }

        public string GetGoogleDriveData() => googleDriveReader.GetAllDriveData();
    }
}
