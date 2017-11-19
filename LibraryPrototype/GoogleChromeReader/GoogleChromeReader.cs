using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleChromeReader.Model;
using LibraryShared.Interfaces.Disk;
using LibraryShared.Interfaces.Readers.Browsers;

namespace GoogleChromeReader
{
	public interface IGoogleChromeReader : IBrowsingHistoryReader<ChromeHistoryEntry>
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
					var test = (long) reader["visit_time"];
					var time = new DateTime(1601, 1, 1).AddSeconds(test / 1_000_000);
					yield return new ChromeHistoryEntry(time, reader["url"] as string, reader["title"] as string);
				}
				conn.Close();
			}
		}

		private string HistoryDbPath => _disk.GetLocalFilePath($@"Users/{_userName}/AppData/Local/Google/Chrome/User Data/Default/History");
	}
}
