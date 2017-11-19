using LibraryPrototype;
using LibraryShared;
using System;
using System.Linq;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
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

				//GoogleDriveTest(disk, userName);

				GoogleChromeTest(disk, userName);

			}
			catch (ArgumentException e)
	        {
		        Console.WriteLine("Incorrect Path!");
		        Console.WriteLine($"Error:  {e.Message}");
			}
            catch (Exception e)
            {
                Console.WriteLine("Couldn't read this disk!");
	            Console.WriteLine($"Error:  {e.Message}");
            }
            
            Console.ReadLine();
        }

		private static void GoogleChromeTest(LibraryShared.Interfaces.Disk.IDisk disk, string userName)
		{
			var googleChromeReader = new GoogleChromeReader.GoogleChromeReader(disk, userName);
			var historyEntries = googleChromeReader.GetHistoryEntries();

			Console.WriteLine($"{"TIME".PadRight(30)} {"URL".PadRight(30)} {"TITLE".PadRight(30)}");

			foreach (var entry in historyEntries)
			{
				Console.WriteLine($"{entry.EntryTime.ToShortDateString().PadRight(30)} {entry.Url.PadRight(30)} {entry.Title.PadRight(30)}");
			}
		}

		private static void GoogleDriveTest(LibraryShared.Interfaces.Disk.IDisk disk, string userName)
		{
			var googleDriveReader = new GoogleDriveReader.GoogleDriveReader(disk, userName);
			var fileActions = googleDriveReader.GetEntries(GoogleDrive.Action.CREATE);

			//var metadata = googleDriveReader.GetMetadata();
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
	}
}
