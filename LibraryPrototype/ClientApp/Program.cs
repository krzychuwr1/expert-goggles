using LibraryPrototype;
using LibraryShared;
using System;
using System.Linq;

namespace ClientApp
{
    class Program
    {
	    private const int SPad = 15;

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

			//Console.WriteLine($"{"TIME".PadRight(SPad)} {"URL".PadRight(SPad)} {"TITLE".PadRight(SPad)}");

			//foreach (var entry in historyEntries)
			//{
			//	Console.WriteLine($"{entry.EntryTime.ToShortDateString().PadRight(SPad)} {entry.Url.PadRight(SPad)} {entry.Title.PadRight(SPad)}");
			//}

			//var downloadEntries = googleChromeReader.GetDownloadEntries();

			//Console.WriteLine($"{"URL".PadRight(70)} {"PATH".PadRight(70)} {"DOWNLOADED SIZE".PadRight(SPad)} {"TOTAL SIZE".PadRight(SPad)} {"STATE".PadRight(SPad)} {"START TIME".PadRight(25)} {"END TIME".PadRight(25)}");

			//foreach (var entry in downloadEntries)
			//{
			//	Console.WriteLine($"{entry.Url.PadRight(70)} {entry.Path.PadRight(70)} {entry.DownloadedSizeKb.ToString().PadRight(SPad)} {entry.TotalSizeKb.ToString().PadRight(SPad)} {entry.State.ToString().PadRight(SPad)} {entry.StartTime.ToString().PadRight(25)} {entry.EndTime.ToString().PadRight(25)}");
			//}

			var searchEntries = googleChromeReader.GetSearchTermEntries();

			Console.WriteLine($"{"TERM".PadRight(80)} {"LAST SEARCH TIME".PadRight(25)} {"COUNT".PadRight(10)}");
			foreach (var entry in searchEntries)
			{
				Console.WriteLine($"{entry.Term.PadRight(80)} {entry.LastSearchTime.ToString().PadRight(25)} {entry.Count.ToString().PadRight(10)}");
			}
		}

		private static void GoogleDriveTest(LibraryShared.Interfaces.Disk.IDisk disk, string userName)
		{
			var googleDriveReader = new GoogleDriveReader.GoogleDriveReader(disk, userName);
			var fileActions = googleDriveReader.GetEntries(GoogleDrive.Action.CREATE);
			//var metadata = googleDriveReader.GetMetadata();
			Console.WriteLine("FILENAME".PadRight(SPad) + "ACTION".PadRight(10) + "DIRECTION".PadRight(10) +
							  "TIME".PadRight(25) + "PATH");

			foreach (var log in fileActions)
			{
				Console.WriteLine(log.FileName.PadRight(SPad)
								  + log.Action.ToString().PadRight(10)
								  + log.Direction.ToString().PadRight(10)
								  + log.Date.ToString().PadRight(25)
								  + log.Path);
			}
		}
	}
}
