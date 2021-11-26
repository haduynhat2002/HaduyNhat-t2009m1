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
    public class ContactModel
    {
        private static string _selectStatementWithConditionTemplate = "SELECT * FROM contacts WHERE Name like @keyword";
        public ContactModel()
        {
            DatabaseMigration.UpdateDatabase();
        }
        public bool Save(Contact contact)
        {
            try
            {
                using (SqliteConnection cnn = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    SqliteCommand command = new SqliteCommand("INSERT INTO notes (Name, PhoneNumber)" +
                " values (@name, @phoneNumber)", cnn);
                    command.Parameters.AddWithValue("@name", contact.Name);
                    command.Parameters.AddWithValue("@phoneNumber", contact.PhoneNumber);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Contact> FindAll()
        {
            List<Contact> result = new List<Contact>();
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
                        var contact = new Contact()
                        {
                            Name = name,
                            PhoneNumber = phoneNumber,
                        };
                        result.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public List<Contact> SearchByKeyword(string keyword)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                //
                using (SqliteConnection cnn = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    //Tạo câu lệnh
                    SqliteCommand cmd = new SqliteCommand(_selectStatementWithConditionTemplate, cnn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    //Bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {                       
                        var name = Convert.ToString(reader["Name"]);
                        var contact = new Contact()
                        {                            
                            Name = name,                            
                        };

                        contacts.Add(contact);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return contacts;
        }

    }
}
