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

            Console.WriteLine("LOGS:");

            foreach(var log in library.GetGoogleDriveReader().GetFileActionsForImage(@"C:\Users\kwrona\Documents\inzynierka\obraz1.dd"))
            {
                Console.WriteLine(log.FileName.PadRight(20)
                    + log.Action.ToString().PadRight(10)
                    + log.Direction.ToString().PadRight(10)
                    + log.Date.ToString().PadRight(25)
                    + log.Path);
            }
            
            Console.ReadLine();
        }
    }
}
