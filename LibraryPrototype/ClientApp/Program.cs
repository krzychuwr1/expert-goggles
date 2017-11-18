using LibraryPrototype;
using LibraryShared;
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

            Console.WriteLine("Provide disk image location:");

            var path = Console.ReadLine();
            try
            {
                var disk = new DiskProvider().OpenDisk();
                var fileActions = library.GetGoogleDriveReader(disk).GetData(GoogleDrive.Action.CREATE);
                Console.WriteLine("FILENAME".PadRight(20) + "ACTION".PadRight(10) + "DIRECTION".PadRight(10) + "TIME".PadRight(25) + "PATH");

                foreach(var log in fileActions)
                {
                    Console.WriteLine(log.FileName.PadRight(20)
                        + log.Action.ToString().PadRight(10)
                        + log.Direction.ToString().PadRight(10)
                        + log.Date.ToString().PadRight(25)
                        + log.Path);
                }
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Incorrect Path!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't read this disk!");
            }
            
            Console.ReadLine();
        }
    }
}
