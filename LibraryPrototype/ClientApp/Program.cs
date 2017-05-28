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
            Console.WriteLine(library.GetGoogleDriveData());
            Console.ReadLine();
        }
    }
}
