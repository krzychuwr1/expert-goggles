using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirefoxReader.Model;
using LibraryShared.Interfaces.Disk;
using LibraryShared.Interfaces.Readers.Browsers;

namespace FirefoxReader
{
	public interface IFirefoxReader : IBrowsingHistoryReader<FirefoxHistoryEntry>, IBookmarksReader<FirefoxBookmarkEntry>, ICookiesReader<FirefoxCookieEntry>
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

		public IEnumerable<FirefoxHistoryEntry> GetHistoryEntries()
		{
			var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath()}\places.sqlite");

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

		private string GetProfilePath()
		{
			var fileStream = _disk.GetFile(ProfilesFilePath);
			var streamReader = new StreamReader(fileStream);
			while (!streamReader.EndOfStream)
			{
				var line = streamReader.ReadLine();
				if (line.StartsWith("Path"))
				{
					return $@"{FirefoxHomePath}\{line.Remove(0, 5)}";
				}
			}
			return string.Empty;
		}

		private string ProfilesFilePath => $@"{FirefoxHomePath}\profiles.ini";

	    private string FirefoxHomePath => $@"Users\{_userName}\AppData\Roaming\Mozilla\Firefox";

	    public IEnumerable<FirefoxBookmarkEntry> GetBookmarkEntries()
	    {
		    var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath()}\places.sqlite");

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

	    public IEnumerable<FirefoxCookieEntry> GetCookies()
	    {
		    var placesDbPath = _disk.GetLocalFilePath($@"{GetProfilePath()}\cookies.sqlite");

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
    }
}
