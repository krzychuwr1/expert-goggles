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
                Console.WriteLine($"{log.FileName} {log.Action} {log.Direction} {log.Date} {log.Path}");
            }
            
            Console.ReadLine();
        }
    }
}
