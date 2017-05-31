using LibraryPrototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var library = new LibraryMain();
            //library.InitGoogleDrive();
            //Console.WriteLine(library.GetGoogleDriveDataSummary());

            Console.WriteLine("FILES:");

            library.SleuthkitTest();
            Console.ReadLine();
        }
    }
}
