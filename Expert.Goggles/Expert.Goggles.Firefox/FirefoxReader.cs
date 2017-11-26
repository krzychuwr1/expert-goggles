using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Expert.Goggles.Core.Interfaces.Disk;
using Expert.Goggles.Core.Interfaces.Readers;
using Expert.Goggles.Core.Interfaces.Readers.Browsers;
using Expert.Goggles.Firefox.Model;
namespace Expert.Goggles.Firefox
{
	public interface IFirefoxReader : 
		IBrowsingHistoryReader<FirefoxHistoryEntry>, 
		IBookmarksReader<FirefoxBookmarkEntry>, 
		ICookiesReader<FirefoxCookieEntry>, 
		IDownloadsReader<FirefoxDownloadEntry>, 
		IMetadataReader<FirefoxMetadata>, 
		IUserBrowsingHistoryReader<FirefoxHistoryEntry>,
		IUserBookmarksReader<FirefoxBookmarkEntry>,
		IUserCookiesReader<FirefoxCookieEntry>,
		IUserDownloadsReader<FirefoxDownloadEntry>
	{
	}

	public class FirefoxReader : IFirefoxReader
    {
	    private readonly IDisk _disk;
	    private readonly string _userName;

	    public FirefoxReader(IDisk disk, string userName)
	    {
		    _disk = disk;
		    _userName = userName;
	    }

	    private IEnumerable<string> GetUsers()
	    {
			var fileStream = _disk.GetFile(ProfilesFilePath);
		    var streamReader = new StreamReader(fileStream);
		    while (!streamReader.EndOfStream)
		    {
			    var line = streamReader.ReadLine();
			    if (line.StartsWith(@"Path=Profiles/"))
			    {
				    yield return line.Remove(0, @"Path=Profiles/".Length);
			    }
		    }
		}

		public IEnumerable<FirefoxHistoryEntry> GetHistoryEntries() => GetHistoryEntries(GetUsers().First());

	    public IEnumerable<FirefoxBookmarkEntry> GetBookmarkEntries() => GetBookmarkEntries(GetUsers().First());

	    public IEnumerable<FirefoxCookieEntry> GetCookies() => GetCookies(GetUsers().First());

	    public IEnumerable<FirefoxDownloadEntry> GetDownloadEntries() => GetDownloadEntries(GetUsers().First());

		private string GetProfilePath(string username) => $@"{FirefoxHomePath}\Profiles\{username}";

		private string ProfilesFilePath => $@"{FirefoxHomePath}\profiles.ini";

	    private string FirefoxHomePath => $@"Users\{_userName}\AppData\Roaming\Mozilla\Firefox";

	    public IEnumerable<FirefoxBookmarkEntry> GetBookmarkEntries(string username)
	    {
		    var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath(username)}\places.sqlite");

		    using (var conn = new SQLiteConnection($"Data Source={placesDbPath}"))
		    {
			    conn.Open();
			    string sql = "SELECT * FROM moz_bookmarks INNER JOIN moz_places ON moz_bookmarks.fk = moz_places.id";
			    SQLiteCommand command = new SQLiteCommand(sql, conn);
			    SQLiteDataReader reader = command.ExecuteReader();
			    while (reader.Read())
			    {
				    var lastModified = DateTimeOffset.FromUnixTimeSeconds((long)reader["lastModified"] / 1_000_000).LocalDateTime;
				    DateTime? lastVisited = null;
				    if (reader["last_visit_date"] is long lastVisit)
				    {
					    lastVisited = DateTimeOffset.FromUnixTimeSeconds(lastVisit / 1_000_000).LocalDateTime;
					}
					yield return new FirefoxBookmarkEntry(reader["url"] as string, reader["title"] as string ?? string.Empty, lastVisited, lastModified, (long)reader["visit_count"]);
			    }
			    conn.Close();
		    }
		}

	    public IEnumerable<FirefoxCookieEntry> GetCookies(string username)
	    {
		    var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath(username)}\cookies.sqlite");

		    using (var conn = new SQLiteConnection($"Data Source={placesDbPath}"))
		    {
			    conn.Open();
			    string sql = "select * from moz_cookies";
			    SQLiteCommand command = new SQLiteCommand(sql, conn);
			    SQLiteDataReader reader = command.ExecuteReader();
			    while (reader.Read())
			    {
				    var creationTime = DateTimeOffset.FromUnixTimeSeconds((long)reader["creationTime"] / 1_000_000).LocalDateTime;
					var lastAccessed = DateTimeOffset.FromUnixTimeSeconds((long)reader["lastAccessed"] / 1_000_000).LocalDateTime;
				    var expiryTime = DateTimeOffset.FromUnixTimeSeconds((long)reader["expiry"] / 1_000_000).LocalDateTime;


				    yield return new FirefoxCookieEntry(reader["baseDomain"] as string, reader["name"] as string, reader["value"] as string, creationTime, lastAccessed, expiryTime);
			    }
			    conn.Close();
		    }
		}

	    public IEnumerable<FirefoxDownloadEntry> GetDownloadEntries(string username)
	    {
		    var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath(username)}\places.sqlite");

		    using (var conn = new SQLiteConnection($"Data Source={placesDbPath}"))
		    {
			    conn.Open();
			    string sql = "select * from moz_annos inner join moz_places on moz_annos.place_id = moz_places.id where moz_annos.anno_attribute_id = 4";
			    SQLiteCommand command = new SQLiteCommand(sql, conn);
			    SQLiteDataReader reader = command.ExecuteReader();
			    while (reader.Read())
			    {
				    var startTime = DateTimeOffset.FromUnixTimeSeconds((long)reader["dateAdded"] / 1_000_000).LocalDateTime;
					yield return new FirefoxDownloadEntry(reader["url"] as string, reader["content"] as string, startTime);
			    }
			    conn.Close();
		    }
		}

	    public FirefoxMetadata GetMetadata() => new FirefoxMetadata(GetUsers());

	    public IEnumerable<FirefoxHistoryEntry> GetHistoryEntries(string username)
	    {
			var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath(username)}\places.sqlite");

		    using (var conn = new SQLiteConnection($"Data Source={placesDbPath}"))
		    {
			    conn.Open();
			    string sql = "select * from moz_historyvisits inner join moz_places on moz_historyvisits.place_id = moz_places.id";
			    SQLiteCommand command = new SQLiteCommand(sql, conn);
			    SQLiteDataReader reader = command.ExecuteReader();
			    while (reader.Read())
			    {
				    var time = DateTimeOffset.FromUnixTimeSeconds((long)reader["visit_date"] / 1_000_000).LocalDateTime;
				    yield return new FirefoxHistoryEntry(time, reader["url"] as string, reader["title"] as string ?? string.Empty);
			    }
			    conn.Close();
		    }
		}
    }
}
