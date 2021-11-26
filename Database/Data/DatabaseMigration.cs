using Microsoft.Data.Sqlite;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Database.Data
{
    public class DatabaseMigration
    {
        private static string _databaseFile = "contact.db";
        public static string _databasePath;
        private static string _createNoteTable = "CREATE TABLE IF NOT EXISTS notes " +
            "(Name NVARCHAR(100) NOT NULL," +
            "PhoneNumber NVARCHAR(255) NOT NULL)";
        public async static void UpdateDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_databaseFile, CreationCollisionOption.OpenIfExists);
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _databaseFile);
            using (SqliteConnection cnn = new SqliteConnection($"Filename={_databasePath}"))
            {
                cnn.Open();
                SqliteCommand command = new SqliteCommand(_createNoteTable, cnn);
                command.ExecuteNonQuery();
            }
        }
    }
}
