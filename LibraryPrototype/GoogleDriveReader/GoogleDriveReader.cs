using LibraryShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace GoogleDrive
{
    public interface IGoogleDriveReader
    {
        string GetAllDriveData();
    }

    public class GoogleDriveReader : IGoogleDriveReader
    {
        private readonly IFileProvider fileProvider;

        public GoogleDriveReader(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public string GetAllDriveData()
        {
            //System.Data.SQLite.
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\kwrona\AppData\Local\Google\Drive\user_default\sync_config.db");
            conn.Open();
            string sql = "select * from data";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
                builder.AppendLine("entry_key" + reader["entry_key"] + "\tdata_value: " + reader["data_value"]);

            return builder.ToString();

        }
    }
}
