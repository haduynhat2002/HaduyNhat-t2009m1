using Database.Data;
using Database.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class NoteModel
    {
        public NoteModel()
        {
            DatabaseMigration.UpdateDatabase();
        }
        public bool Save(Note note)
        {
            try
            {
                using (SqliteConnection cnn = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    SqliteCommand command = new SqliteCommand("INSERT INTO notes (Name, PhoneNumber)" +
                " values (@name, @phoneNumber)", cnn);                    
                    command.Parameters.AddWithValue("@name", note.Name);                                       
                    command.Parameters.AddWithValue("@phoneNumber", note.PhoneNumber);                                       
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Note> FindAll()
        {
            List<Note> result = new List<Note>();
            try
            {
                // mo ket noi den data base
                using (SqliteConnection cnn = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    SqliteCommand command = new SqliteCommand("SELECT * FROM notes", cnn);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var phoneNumber = reader.GetString(1);                      
                        var note = new Note()
                        {
                            Name = name,
                            PhoneNumber = phoneNumber,                          
                        };
                        result.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

    }
}
