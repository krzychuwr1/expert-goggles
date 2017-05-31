﻿using LibraryShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Win32;
using GoogleDrive.Exceptions;

namespace GoogleDrive
{
    public interface IGoogleDriveReader
    {
        void Init();

        string UserEmail { get; }

        string AllData { get; }

        string AppVersion { get; }

        IEnumerable<string> Filenames {get;}

        string GetCrucialDataSummary();

        void SleuthKitTest();
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

        public IEnumerable<string> SleuthKitTest()
        public void SleuthKitTest()
        {
            var disk = fileProvider.OpenDisk(@"D:\Obrazy\obraz1.dd");

            foreach(var userName in disk.GetAllUsers())
            {
                Console.WriteLine(userName);
            }
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

        private void GetSnapshotData(string drivePath)
        {
            var sync_configdbPath = $@"{drivePath}user_default\snapshot.db";

            SQLiteConnection conn = new SQLiteConnection($"Data Source={sync_configdbPath}");
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
