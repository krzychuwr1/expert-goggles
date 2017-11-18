using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using GoogleDrive;
using GoogleDrive.Exceptions;
using LibraryShared;
using Microsoft.Win32;
using Action = GoogleDrive.Action;

namespace GoogleDriveReader
{
    public interface IGoogleDriveReader
    {
        void Init();

        string UserEmail { get; }

        string AllData { get; }

        string AppVersion { get; }

        IEnumerable<string> Filenames {get;}

        string GetCrucialDataSummary();

        IEnumerable<FileActionEntry> GetFileActionsForImage(string diskPath, Action? actionType = null, Direction? direction = null);
    }

    public class GoogleDriveReader : IGoogleDriveReader
    {
        private readonly IFileProvider fileProvider;

        public string UserEmail { get; private set; }

        public string AllData { get; private set; }

        public string AppVersion { get; private set; }

        private List<string> filenames = new List<string>();

        public IEnumerable<string> Filenames { get => filenames; }

        public GoogleDriveReader(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public void Init()
        {
            var drivePath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Google\Drive", "Path", null) as string;

            AppVersion = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Google\Drive", "FileManagerRestartedVersion", null) as string;

            if (drivePath is null)
            {
                throw new NoRegistryKeyException("Google drive path not found");
            }

            GetSyncConfigData(drivePath);
            GetSnapshotData(drivePath);
        }

	    private string FindLogFile(FileProviderDisk disk)
	    {
			var userName = disk.GetAllUsers().Single();

		    return $@"Users/{userName}/AppData/Local/Google/Drive/user_default/sync_log.log";
	    }

		public IEnumerable<FileActionEntry> GetFileActionsForImage(string diskPath, Action? actionType = null, Direction? direction = null)
        {
            var disk = fileProvider.OpenDisk(diskPath);

            var stream = disk.GetFile(FindLogFile(disk));

            var result = LogReader.GetFilesHistoryFromLogs(stream);


            if (actionType != null) result = result.Where(entry => entry.Action == actionType);
            if (direction != null) result = result.Where(entry => entry.Direction == direction);

            return result;
            
        }

        private void GetSyncConfigData(string drivePath)
        {
            var sync_configdbPath = $@"{drivePath}user_default\sync_config.db";

            SQLiteConnection conn = new SQLiteConnection($"Data Source={sync_configdbPath}");
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
                    UserEmail = entryValue;
                }
            }

            conn.Close();
            AllData = builder.ToString();
        }

        private void GetFilesHistoryFromLogs(string logsPath)
        {
            File.ReadLines(logsPath);
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

                filenames.Add(filename);
            }

            conn.Close();
            AllData = builder.ToString();
        }

        public string GetCrucialDataSummary()
            => $"Email: {UserEmail} {Environment.NewLine}" +
               $"AppVersion: {AppVersion}";
        
    }
}
