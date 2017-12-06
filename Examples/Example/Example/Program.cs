using System;
using System.Collections.Generic;
using System.Linq;
using Expert.Goggles.Chrome.DiskExtensions;
using Expert.Goggles.Core.Disk;
using Expert.Goggles.Core.Interfaces.Disk;
using Expert.Goggles.Core.Model;
using Expert.Goggles.Detector;
using Expert.Goggles.Detector.DiskExtensions;
using Expert.Goggles.Firefox.DiskExtensions;
using Expert.Goggles.GoogleDrive.DiskExtensions;
using Expert.Goggles.Skype.DiskExtensions;
using Action = Expert.Goggles.GoogleDrive.Action;

namespace Example
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
				var detector = disk.GetDetector();
				var users = detector.GetWindowsUsers().ToList();

				if (!users.Any())
				{
					Console.WriteLine("No users on this disk!");
					Console.ReadKey();
					return;
				}

				Console.WriteLine("Found usernames:");
				foreach (var user in users)
				{
					Console.WriteLine(user);
				}
				string userName = null;
				while (users.All(u => u != userName))
				{
					Console.WriteLine("Provide one of found usernames: ");
					userName = Console.ReadLine();
				}

				var apps = detector.GetAppsForWindowsUser(userName).ToList();

				while (true)
				{
					Console.WriteLine();
					Console.WriteLine("Found apps:");
					foreach (var app in apps)
					{
						Console.WriteLine(app);
					}

					if (!apps.Any())
					{
						Console.WriteLine("No apps for this user!");
						Console.ReadKey();
						return;
					}

					string appName = null;
					while (apps.All(u => u != appName) && appName != "q")
					{
						Console.WriteLine("Provide one of found app names: (or write q to quit)");
						appName = Console.ReadLine()?.Trim();
					}

					switch (appName)
					{
						case AppNames.Skype: SkypeTest(disk, userName); break;
						case AppNames.Chrome: GoogleChromeTest(disk, userName); break;
						case AppNames.Firefox: FirefoxTest(disk, userName); break;
						case AppNames.GoogleDrive: GoogleDriveTest(disk, userName); break;
						case "q": return;
					}
				}
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

		private static void SkypeTest(IDisk disk, string userName)
		{
			var skypeReader = disk.GetSkypeReader(userName);

			var metadata = skypeReader.GetMetadata();

			var users = skypeReader.GetMetadata().Users;

			Console.WriteLine("Found usernames:");
			foreach (var user in users)
			{
				Console.WriteLine(user);
			}
			string skypeUsername = null;
			while (users.All(u => u != skypeUsername))
			{
				Console.WriteLine("Provide one of found usernames: ");
				skypeUsername = Console.ReadLine();
			}

			Console.WriteLine("What do you want to display? ca - calls, co - contacts, m - messages");

			var choice = Console.ReadLine()?.Trim();
			switch (choice)
			{
                case "ca":
                    var calls = skypeReader.GetCallEntries(skypeUsername);
                    PrintEntries(calls, $"{"HOST".PadRight(40)} {"TOPIC".PadRight(40)} {"START TIME".PadRight(25)} {"ACTIVE MEMBERS".PadRight(25)}",
                        call => Console.WriteLine($"{call.HostIdentity.PadRight(40)} {call.Topic.PadRight(40)} {call.BeginTimestamp?.ToString().PadRight(25)} {call.ActiveMembers.ToString().PadRight(25)}"));
                    break;
                case "co":
					var contacts = skypeReader.GetContactEntries(skypeUsername);
                    PrintEntries(contacts, $"{"FULL NAME".PadRight(30)} {"CITY".PadRight(30)} {"SKYPE NAME".PadRight(25)} {"PHONE".PadRight(25)}",
                        contact => Console.WriteLine($"{contact.FullName.PadRight(30)} {contact.City.PadRight(30)} {contact.SkypeName.PadRight(25)} {contact.PhoneNumber.PadRight(25)}"));
					break;
				case "m":
					var messages = skypeReader.GetMessagesEntries(skypeUsername);
                    PrintEntries(messages, $"{"AUTHOR".PadRight(30)} {"AUTHOR DISPLAY".PadRight(30)} {"CHATNAME".PadRight(30)} {"TIME".PadRight(30)} {"MESSAGE".PadRight(30)}",
                        message => Console.WriteLine($"{message.Author.PadRight(30)} {message.AuthorDisplayName.PadRight(30)} {message.Chatname.PadRight(30)} {message.Timestamp?.ToString().PadRight(30)} {message.Content.PadRight(30)}"));
					break;
			}
		}

        private static void PrintEntries<T>(IEnumerable<T> calls, string header, Action<T> format)
        {
            var batchNumber = 0;
            while (true)
            {
                int printedEntries = 0;
                Console.WriteLine(header);
                foreach (var call in calls.Skip(batchNumber * 20).Take(20))
                {
                    format(call);
                    
                    printedEntries++;
                }
                if (printedEntries == 0 || Console.ReadKey().KeyChar == 'q')
                {
                    break;
                }
                batchNumber++;
            }
            batchNumber = 0;
        }

        private static void FirefoxTest(IDisk disk, string userName)
		{
			var firefoxReader = disk.GetFirefoxReader(userName);

			Console.WriteLine("What do you want to display? h - history, b - bookmarks, c - cookies, d - downloads");

			var choice = Console.ReadLine()?.Trim();
			switch (choice)
			{
				case "h":
					var historyEntries = firefoxReader.GetHistoryEntries();
					PrintHistoryEntries(historyEntries);
					break;
				case "b":
					var bookmarksEntries = firefoxReader.GetBookmarkEntries();
                    PrintEntries(bookmarksEntries, $"{"URL".PadRight(70)} {"TITLE".PadRight(40)} {"LAST MODIFIED".PadRight(25)} {"LAST VISITED".PadRight(25)} {"VISITS COUNT".PadRight(15)}",
                        bookmarkEntry => Console.WriteLine($"{bookmarkEntry.Url.PadRight(70)} {bookmarkEntry.Title.PadRight(40)} {bookmarkEntry.LastModified.ToString().PadRight(25)} {bookmarkEntry.LastVisited.ToString().PadRight(25)} {bookmarkEntry.VisitCount.ToString().PadRight(15)} "));
					break;
				case "c":
					var cookies = firefoxReader.GetCookies();
                    PrintEntries(cookies, $"{"DOMAIN".PadRight(30)} {"NAME".PadRight(30)}",
                        cookie => Console.WriteLine($"{cookie.Url.PadRight(30)} {cookie.Name.PadRight(30)}"));
					break;
				case "d":
					var downloads = firefoxReader.GetDownloadEntries();
                    PrintEntries(downloads, $"{"URL".PadRight(100)} {"PATH".PadRight(80)} {"START TIME".PadRight(25)}",
                        download => Console.WriteLine($"{download.Url.PadRight(100)} {download.Path.PadRight(80)} {download.StartTime.ToString().PadRight(25)}"));
					break;
			}
		}

		private static void GoogleChromeTest(IDisk disk, string userName)
		{
			var googleChromeReader = disk.GetGoogleChromeReader(userName);

			Console.WriteLine("What do you want to display? h - history, s - search terms, d - downloads");

			var choice = Console.ReadLine()?.Trim();
			switch (choice)
			{
				case "h":
					var historyEntries = googleChromeReader.GetHistoryEntries();
					PrintHistoryEntries(historyEntries);
					break;
				case "s":
					var searchEntries = googleChromeReader.GetSearchTermEntries();
                    PrintEntries(searchEntries, $"{"TERM".PadRight(80)} {"LAST SEARCH TIME".PadRight(25)} {"COUNT".PadRight(10)}",
                        entry => Console.WriteLine($"{entry.Term.PadRight(80)} {entry.LastSearchTime.ToString().PadRight(25)} {entry.Count.ToString().PadRight(10)}"));
					break;
				case "d":
					var downloadEntries = googleChromeReader.GetDownloadEntries();
                    PrintEntries(downloadEntries, $"{"URL".PadRight(70)} {"PATH".PadRight(70)} {"DOWNLOADED SIZE".PadRight(SPad)} {"TOTAL SIZE".PadRight(SPad)} {"STATE".PadRight(SPad)} {"START TIME".PadRight(25)} {"END TIME".PadRight(25)}",
                        entry => Console.WriteLine($"{entry.Url.PadRight(70)} {entry.Path.PadRight(70)} {entry.DownloadedSizeKb.ToString().PadRight(SPad)} {entry.TotalSizeKb.ToString().PadRight(SPad)} {entry.State.ToString().PadRight(SPad)} {entry.StartTime.ToString().PadRight(25)} {entry.EndTime.ToString().PadRight(25)}"));
					break;
			}
		}

		private static void GoogleDriveTest(IDisk disk, string userName)
		{
			var googleDriveReader = disk.GetGoogleDriverReader(userName);

			Console.WriteLine("What do you want to display? h - history, b - bookmarks, c - cookies, d - downloads");

			var choice = Console.ReadLine()?.Trim();
			switch (choice)
			{
				case "h":
					break;
				case "b":
					break;
				case "c":
					break;
				case "d":
					break;
			}

			var fileActions = googleDriveReader.GetEntries(Action.CREATE);
            //var metadata = googleDriveReader.GetMetadata();
            PrintEntries(fileActions, "FILENAME".PadRight(SPad) + "ACTION".PadRight(10) + "DIRECTION".PadRight(10) +
                              "TIME".PadRight(25) + "PATH",
                              log => Console.WriteLine(log.FileName.PadRight(SPad)
                                  + log.Action.ToString().PadRight(10)
                                  + log.Direction.ToString().PadRight(10)
                                  + log.Date.ToString().PadRight(25)
                                  + log.Path));
		}

		private static void PrintHistoryEntries(IEnumerable<IHistoryEntry> entries)
		{
            PrintEntries(entries, $"{"TIME".PadRight(25)} {"URL".PadRight(50)} {"TITLE".PadRight(50)}",
                entry => Console.WriteLine($"{entry.EntryTime.ToString().PadRight(25)} {entry.Url.PadRight(50)} {entry.Title.PadRight(50)}"));
		}
	}
}
