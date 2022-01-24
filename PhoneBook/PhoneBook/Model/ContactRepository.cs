using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook.Model
{
    public class ContactRepository
    {
        static object locker = new object();
        SQLiteConnection database;

        public ContactRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            //database.DropTable<Contact>();
            database.CreateTable<Contact>();
        }

        public IEnumerable<Contact> GetItems()
        {
            lock (locker)
                return database.Table<Contact>().ToList();
        }

        public Contact GetItem(int id)
        {
            lock (locker)
                return database.Get<Contact>(id);
        }

        public int DeleteItem(int id)
        {
            lock (locker)
                return database.Delete<Contact>(id);
        }

        public Contact GetContact(string str)
        {
            lock (locker)
            {
                return database.Table<Contact>().FirstOrDefault(contact => contact.PhoneNumber == str);
            }
        }

        public int SaveItem(Contact item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }
    }
}
