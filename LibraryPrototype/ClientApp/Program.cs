using System;
using System.Collections.Generic;
using System.Linq;
using Expert.Goggles.Chrome;
using Expert.Goggles.Core.Disk;
using Expert.Goggles.Core.Interfaces.Disk;
using Expert.Goggles.Core.Model;
using Expert.Goggles.Firefox;
using Expert.Goggles.GoogleDrive;
using Action = Expert.Goggles.GoogleDrive.Action;

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

                GoogleDriveTest(disk, userName);

    //            //GoogleChromeTest(disk, userName);
    //            var r = new SkypeReader.SkypeReader(disk, userName);
				//var test = r.GetMetadata();
				//var ce = r.GetMessagesEntries(test.Users.First()).ToList();
				//FirefoxTest(disk, userName);

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

	    private static void FirefoxTest(IDisk disk, string userName)
	    {
		    var firefoxReader = new FirefoxReader(disk, userName);

		    var historyEntries = firefoxReader.GetHistoryEntries();

			PrintHistoryEntries(historyEntries);

		    var bookmarksEntries = firefoxReader.GetBookmarkEntries();

		    Console.WriteLine($"{"URL".PadRight(70)} {"TITLE".PadRight(40)} {"LAST MODIFIED".PadRight(25)} {"LAST VISITED".PadRight(25)} {"VISITS COUNT".PadRight(15)}");

			foreach (var bookmarkEntry in bookmarksEntries)
		    {
			    Console.WriteLine($"{bookmarkEntry.Url.PadRight(70)} {bookmarkEntry.Title.PadRight(40)} {bookmarkEntry.LastModified.ToString().PadRight(25)} {bookmarkEntry.LastVisited.ToString().PadRight(25)} {bookmarkEntry.VisitCount.ToString().PadRight(15)} ");
		    }

		    var cookies = firefoxReader.GetCookies();

			Console.WriteLine($"{"DOMAIN".PadRight(30)} {"NAME".PadRight(30)}");

		    foreach (var cookie in cookies)
		    {
			    Console.WriteLine($"{cookie.Url.PadRight(30)} {cookie.Name.PadRight(30)}");
		    }

		    var downloads = firefoxReader.GetDownloadEntries();

		    Console.WriteLine($"{"URL".PadRight(100)} {"PATH".PadRight(80)} {"START TIME".PadRight(25)}");

		    foreach (var download in downloads)
		    {
			    Console.WriteLine($"{download.Url.PadRight(100)} {download.Path.PadRight(80)} {download.StartTime.ToString().PadRight(25)}");
		    }
	    }

	    private static void GoogleChromeTest(IDisk disk, string userName)
		{
			var googleChromeReader = new GoogleChromeReader(disk, userName);

			var historyEntries = googleChromeReader.GetHistoryEntries();

			PrintHistoryEntries(historyEntries);

			var downloadEntries = googleChromeReader.GetDownloadEntries();

			Console.WriteLine($"{"URL".PadRight(70)} {"PATH".PadRight(70)} {"DOWNLOADED SIZE".PadRight(SPad)} {"TOTAL SIZE".PadRight(SPad)} {"STATE".PadRight(SPad)} {"START TIME".PadRight(25)} {"END TIME".PadRight(25)}");

			foreach (var entry in downloadEntries)
			{
				Console.WriteLine($"{entry.Url.PadRight(70)} {entry.Path.PadRight(70)} {entry.DownloadedSizeKb.ToString().PadRight(SPad)} {entry.TotalSizeKb.ToString().PadRight(SPad)} {entry.State.ToString().PadRight(SPad)} {entry.StartTime.ToString().PadRight(25)} {entry.EndTime.ToString().PadRight(25)}");
			}

			var searchEntries = googleChromeReader.GetSearchTermEntries();

			Console.WriteLine($"{"TERM".PadRight(80)} {"LAST SEARCH TIME".PadRight(25)} {"COUNT".PadRight(10)}");
			foreach (var entry in searchEntries)
			{
				Console.WriteLine($"{entry.Term.PadRight(80)} {entry.LastSearchTime.ToString().PadRight(25)} {entry.Count.ToString().PadRight(10)}");
			}
		}

		private static void GoogleDriveTest(IDisk disk, string userName)
		{
			var googleDriveReader = new GoogleDriveReader(disk, userName);
			var fileActions = googleDriveReader.GetEntries(Action.CREATE);
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

	    private static void PrintHistoryEntries(IEnumerable<IHistoryEntry> entries)
	    {
			Console.WriteLine($"{"TIME".PadRight(25)} {"URL".PadRight(50)} {"TITLE".PadRight(50)}");

		    foreach (var entry in entries)
		    {
			    Console.WriteLine($"{entry.EntryTime.ToString().PadRight(25)} {entry.Url.PadRight(50)} {entry.Title.PadRight(50)}");
		    }
		}
	}
}
