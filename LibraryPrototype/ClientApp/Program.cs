using LibraryPrototype;
using LibraryShared;
using System;
using System.Collections.Generic;
using System.IO;
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

            Console.WriteLine("Provide disk image location: ");

            var path = Console.ReadLine();
	        try
	        {
		        var disk = string.IsNullOrWhiteSpace(path)
			        ? new DiskProvider().OpenDisk()
			        : new DiskProvider().OpenDisk(path);
		        var users = disk.GetAllUsers();
		        Console.WriteLine("Found usernames:");
		        foreach (var user in users)
		        {
			        Console.WriteLine(user);
		        }
		        string userName = null;
		        while (users.Any(u => u != userName))
		        {
			        Console.WriteLine("Provide one of found usernames: ");
			        userName = Console.ReadLine();
		        }

		        GoogleDriveReader.IGoogleDriveReader googleDriveReader = library.GetGoogleDriveReader(disk, userName);
		        var fileActions = googleDriveReader.GetData(GoogleDrive.Action.CREATE);
		        var metadata = googleDriveReader.GetMetadata();
		        Console.WriteLine("FILENAME".PadRight(30) + "ACTION".PadRight(10) + "DIRECTION".PadRight(10) +
		                          "TIME".PadRight(25) + "PATH");

		        foreach (var log in fileActions)
		        {
			        Console.WriteLine(log.FileName.PadRight(30)
			                          + log.Action.ToString().PadRight(10)
			                          + log.Direction.ToString().PadRight(10)
			                          + log.Date.ToString().PadRight(25)
			                          + log.Path);
		        }
	        }
	        catch (ArgumentException e)
	        {
		        Console.WriteLine("Incorrect Path!");
	        }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't read this disk!");
	            Console.WriteLine($"Error:  {e.Message}");
            }
            
            Console.ReadLine();
        }
    }
}
