using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhoneBook
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            contactsList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }

        private async void SyncButton_Click(object sender, EventArgs e)
        {
            await GetContacts();
        }

        private async Task GetContacts()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.ContactsRead>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.ContactsRead>();
            }

            syncTxt.Text = "Синхронизация...";
            await Task.Run(() => Thread.Sleep(100));

            syncTxt.Text = StrSync(await GetContactsAsync());
            contactsList.ItemsSource = App.Database.GetItems();

            await Task.Run(() => Thread.Sleep(2000));
            syncTxt.Text = "";
        }

        private async Task<int> GetContactsAsync()
        {
            try
            {
                var contacts = await Contacts.GetAllAsync();

                if (contacts == null)
                    return 2;

                foreach (Contact con in contacts)
                {
                    PhoneBook.Model.Contact contact = new PhoneBook.Model.Contact();

                    contact.FirstName = con.GivenName;
                    contact.LastName = con.FamilyName;
                    contact.PhoneNumber = con.Phones[0].PhoneNumber;

                    if (App.Database.GetContact(contact.PhoneNumber) == null || App.Database.GetContact(contact.PhoneNumber).PhoneNumber != contact.PhoneNumber)
                        App.Database.SaveItem(contact);
                }


                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
            
        }

        private string StrSync(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        return "Ошибка";
                    }
                case 1:
                    {
                        return "Синхронизировано";
                    }
                case 2:
                    {
                        return "Контактов нет";
                    }
                default:
                    {
                        return "Ошибка";
                    }
            }
        }
    }
}
