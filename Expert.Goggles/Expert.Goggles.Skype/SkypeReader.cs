﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Expert.Goggles.Core.Interfaces.Disk;
using Expert.Goggles.Core.Interfaces.Readers;
using Expert.Goggles.Core.Interfaces.Readers.Messengers;
using Expert.Goggles.Skype.Model;

namespace Expert.Goggles.Skype
{
    public interface ISkypeReader : IMetadataReader<SkypeMetadata>, ITextMessegesReader<SkypeTextMessageEntry>, IContactsReader<SkypeContactEntry>, ICallsReader<SkypeCallEntry>
    {

    }
    public class SkypeReader : ISkypeReader 
    {
        private readonly IDisk _disk;
        private readonly string _userName;
	    
        public SkypeReader(IDisk disk, string userName)
        {
            _disk = disk;
            _userName = userName;
        }

		private string MainFolderPath => $@"Users\{_userName}\AppData\Local\Packages\Microsoft.SkypeApp_kzf8qxf38zg5c\LocalState";

        private string GetDbPath(string skypeUserName) => _disk.GetLocalFilePath($@"{MainFolderPath}\{skypeUserName}\main.db");

        public IEnumerable<SkypeCallEntry> GetCallEntries(string skypeUsername)
        {
            using (var conn = new SQLiteConnection($"Data Source={GetDbPath(skypeUsername)}"))
            {
                conn.Open();
                string sql = "select * from calls";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                StringBuilder builder = new StringBuilder();
                while (reader.Read())
                {
                    yield return new SkypeCallEntry
                    {
                        ActiveMembers = reader["active_members"] as int?,
                        BeginTimestamp = reader["begin_timestamp"] as DateTime?,
                        HostIdentity = reader["host_identity"] as string,
                        Topic = reader["topic"] as string
                    };
                }
                conn.Close();
            }
        }

        public IEnumerable<SkypeContactEntry> GetContactEntries(string skypeUsername)
        {
            using (var conn = new SQLiteConnection($"Data Source={GetDbPath(skypeUsername)}"))
            {
                conn.Open();
                string sql = "select * from contacts";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new SkypeContactEntry
                    {
                        Birthdate = reader["birthday"] as DateTime?,
                        City = reader["city"] as string,
                        Country = reader["country"] as string,
                        DisplayName = reader["displayname"] as string,
                        FullName = reader["fullname"] as string,
                        MobilePhoneNumber = reader["phone_mobile"] as string,
                        OfficePhoneNumber = reader["phone_office"] as string,
                        PhoneNumber = reader["phone_home"] as string,
                        PstnNumber = reader["pstnnumber"] as string,
                        SkypeName = reader["skypename"] as string
                    };
                }
                conn.Close();
            }
        }

        public IEnumerable<SkypeTextMessageEntry> GetMessagesEntries(string skypeUsername)
        {
            using (var conn = new SQLiteConnection($"Data Source={GetDbPath(skypeUsername)}"))
            {
                conn.Open();
                string sql = "select * from messages";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new SkypeTextMessageEntry
                    {
                        AuthorDisplayName = reader["from_dispname"] as string,
                        Author = reader["author"] as string,
                        Chatname = reader["chatname"] as string,
                        Content = reader["body_xml"] as string,
                        Timestamp = reader["timestamp"] as DateTime?
                    };
                }
                conn.Close();
            }
        }

        public SkypeMetadata GetMetadata()
        {
	        return new SkypeMetadata(MainFolderPath, _disk.GetDirectorySubdirectories(MainFolderPath).Where(p => !_notUserNamesFolders.Contains(p)));
        }

	    private readonly string[] _notUserNamesFolders = {"coexistence", "DataRv", "DiagOutputDir", "logs", "SkypeRT", "Tracing"};
    }
}
