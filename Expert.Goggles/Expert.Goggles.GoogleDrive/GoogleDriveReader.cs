using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Expert.Goggles.Core.Interfaces.Disk;
using Expert.Goggles.Core.Interfaces.Readers;
using Expert.Goggles.Core.Interfaces.Readers.Cloud;

namespace Expert.Goggles.GoogleDrive
{
    public interface IGoogleDriveReader : IMetadataReader<GoogleDriveMetadata>, ICloudEntriesReader<GoogleDriveFileActionEntry>
    {
        IEnumerable<GoogleDriveFileActionEntry> GetEntries(Action? actionType = null, Direction? direction = null);
    }

    public class GoogleDriveReader : IGoogleDriveReader
    {
        private readonly IDisk _disk;
	    private readonly string _userName;

        public GoogleDriveReader(IDisk disk, string userName)
        {
	        _disk = disk;
	        _userName = userName;
        }

	    private string FindLogFile() => $@"Users/{_userName}/AppData/Local/Google/Drive/user_default/sync_log.log";

	    private string FindDbPath() => _disk.GetLocalFilePath($@"Users/{_userName}/AppData/Local/Google/Drive/user_default/sync_config.db");

	    public IEnumerable<GoogleDriveFileActionEntry> GetEntries()
        {
            return GetEntries(null, null);
        }

		public IEnumerable<GoogleDriveFileActionEntry> GetEntries(Action? actionType = null, Direction? direction = null)
        {
            var stream = _disk.GetFile(FindLogFile());

            var result = LogReader.GetFilesHistoryFromLogs(stream);

            if (actionType != null) result = result.Where(entry => entry.Action == actionType);
            if (direction != null) result = result.Where(entry => entry.Direction == direction);

            return result;
        }

        private string GetUserEmail()
        {
            var sync_configdbPath = FindDbPath();

            using (var conn = new SQLiteConnection($"Data Source={sync_configdbPath}"))
            {
                conn.Open();
                string sql = "select * from data";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                StringBuilder builder = new StringBuilder();
                while (reader.Read())
                {
                    var entryKey = reader["entry_key"] as string;
                    var entryValue = reader["data_value"] as string;

                    builder.AppendLine($"key: {entryKey}, value: {entryValue}");
                    if (entryKey == "user_email")
                    {
                        return entryValue;
                    }
                }
                conn.Close(); 
            }
            return string.Empty;
        }

        private void GetSnapshotData(string snapshotPath)
        {
            SQLiteConnection conn = new SQLiteConnection($"Data Source={snapshotPath}");
            conn.Open();
            string sql = "select filename from cloud_entry";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
            {
                var filename = reader["filename"] as string;

                //filenames.Add(filename);
            }

            conn.Close();
            //AllData = builder.ToString();
        }

        public GoogleDriveMetadata GetMetadata()
        {

            return new GoogleDriveMetadata
            {
                UserEmail = GetUserEmail()
            };
        }
	}
}
