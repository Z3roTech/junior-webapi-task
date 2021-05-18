using ST_JuniorProject.Models;
using ST_JuniorProject.Repositories.Interfaces;
using ST_JuniorProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Services.Implementations
{
    public class UserInfoUpdateService : IUserInfoUpdateService
    {
        private IUserRepository<UserData> Users { get; set; }
        private IUserRepository<UserContact> Contacts { get; set; }

        public UserInfoUpdateService(IUserRepository<UserData> users, IUserRepository<UserContact> contacts)
        {
            Users = users;
            Contacts = contacts;
        }

        public void UpdateUserContact(CRMUserInfo userInfo, string phoneNumber)
        {
            // TODO: По login и lineNumber находим Client и создаём новый контакт на Client с PhoneNumber полученным из CRMClient
            UserData user = Users.GetAll().Where(u => u.Login == userInfo.Login && u.LineNumber == userInfo.LineNumber).FirstOrDefault();
            UserContact contact = new UserContact()
            {
                Client = user,
                PhoneNumber = phoneNumber,
                ClientId = user.Id,
            };
            Contacts.Create(contact);
        }
    }
}