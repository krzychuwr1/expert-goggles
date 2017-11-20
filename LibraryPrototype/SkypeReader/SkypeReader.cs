using LibraryShared.Interfaces.Readers;
using LibraryShared.Interfaces.Readers.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SkypeReader.Model;
using LibraryShared.Interfaces.Disk;

namespace SkypeReader
{
    public interface ISkypeReader : IMetadataReader<SkypeMetadata>, ITextMessegesReader<SkypeTextMessegeEntry>, IContactsReader<SkypeContactEntry>, ICallsReader<SkypeCallEntry>
    {

    }
    public class SkypeReader : ISkypeReader 
    {
        private readonly IDisk _disk;
        private readonly string _userName;
        private string _skypeUserName = "michaeldzo";

        public SkypeReader(IDisk disk, string userName)
        {
            _disk = disk;
            _userName = userName;
        }

        private string HistoryDbPath => _disk.GetLocalFilePath($@"Users\{_userName}\AppData\Local\Packages\Microsoft.SkypeApp_kzf8qxf38zg5c\LocalState\{_skypeUserName}\main.db");

        public IEnumerable<SkypeCallEntry> GetCallEntries()
        {
            using (var conn = new SQLiteConnection($"Data Source={HistoryDbPath}"))
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

        public IEnumerable<SkypeContactEntry> GetContactEntries()
        {
            using (var conn = new SQLiteConnection($"Data Source={HistoryDbPath}"))
            {
                conn.Open();
                string sql = "select * from contacts";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                StringBuilder builder = new StringBuilder();
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

        public IEnumerable<SkypeTextMessegeEntry> GetMessegesEntries()
        {
            using (var conn = new SQLiteConnection($"Data Source={HistoryDbPath}"))
            {
                conn.Open();
                string sql = "select * from messages";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                StringBuilder builder = new StringBuilder();
                while (reader.Read())
                {
                    yield return new SkypeTextMessegeEntry
                    {
                        AuthorDisplayName = reader["from_dispname"] as string,
                        AuthorSkypeName = reader["author"] as string,
                        Chatname = reader["chatname"] as string,
                        ContentXml = reader["body_xml"] as string,
                        Timestamp = reader["timestamp"] as DateTime?
                    };
                }
                conn.Close();
            }
        }

        public SkypeMetadata GetMetadata()
        {
            throw new NotImplementedException();
        }
    }
}
