using PhoneBook.Model;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBook
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "contacts.db";
        public static ContactRepository database;
        public static ContactRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContactRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
