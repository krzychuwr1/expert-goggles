using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleChromeReader.Extensions;
using GoogleChromeReader.Model;
using LibraryShared.Interfaces.Disk;
using LibraryShared.Interfaces.Readers.Browsers;

namespace GoogleChromeReader
{
	public interface IGoogleChromeReader : IBrowsingHistoryReader<ChromeHistoryEntry>, IDownloadsReader<ChromeDownloadEntry>
	{
	}

	public class GoogleChromeReader : IGoogleChromeReader
	{
		private readonly IDisk _disk;
		private readonly string _userName;

		public GoogleChromeReader(IDisk disk, string userName)
		{
			_disk = disk;
			_userName = userName;
		}

		public IEnumerable<ChromeHistoryEntry> GetHistoryEntries()
		{
			using (var conn = new SQLiteConnection($"Data Source={HistoryDbPath}"))
			{
				conn.Open();
				string sql = "select v.visit_time, u.url, u.title from  visits v inner join urls u on u.id = v.url order by v.visit_time desc";
				SQLiteCommand command = new SQLiteCommand(sql, conn);
				SQLiteDataReader reader = command.ExecuteReader();
				StringBuilder builder = new StringBuilder();
				while (reader.Read())
				{
					var time = ((long)reader["visit_time"]).ConvertToDateTimeFromChromeTimeStamp();
					yield return new ChromeHistoryEntry(time, reader["url"] as string, reader["title"] as string);
				}
				conn.Close();
			}
		}

		public IEnumerable<ChromeDownloadEntry> GetDownloadEntries()
		{
			using (var conn = new SQLiteConnection($"Data Source={HistoryDbPath}"))
			{
				conn.Open();
				string sql = "select * from downloads";
				SQLiteCommand command = new SQLiteCommand(sql, conn);
				SQLiteDataReader reader = command.ExecuteReader();
				StringBuilder builder = new StringBuilder();
				while (reader.Read())
				{
					var startTime = ((long)reader["start_time"]).ConvertToDateTimeFromChromeTimeStamp();
					var endTime = ((long) reader["end_time"]).ConvertToDateTimeFromChromeTimeStamp();
					var totalSizeKb = (long) reader["received_bytes"] / 1024;
					var downloadedSizeKb = (long) reader["total_bytes"] / 1024;
					var state = (EChromeDownloadState)(long)reader["state"];
					var path = reader["current_path"] as string;
					var url = reader["tab_url"] as string;
					yield return new ChromeDownloadEntry(url, path, startTime, endTime, downloadedSizeKb, totalSizeKb, state);
				}
				conn.Close();
			}
		}

		private string HistoryDbPath => _disk.GetLocalFilePath($@"Users/{_userName}/AppData/Local/Google/Chrome/User Data/Default/History");
	}
}
