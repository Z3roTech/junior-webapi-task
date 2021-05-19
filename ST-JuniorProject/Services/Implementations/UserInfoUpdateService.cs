using Microsoft.Data.SqlClient;
using ST_JuniorProject.Models;
using ST_JuniorProject.Repositories.Interfaces;
using ST_JuniorProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StranzitOnline.Common.Tools;

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
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ClientInformation;Trusted_Connection=True;MultipleActiveResultSets=true";
            int clientId = GetClientId(userInfo, connectionString);
            CreateUserContact(phoneNumber, connectionString, clientId);
        }

        private void CreateUserContact(string phoneNumber, string connectionString, int clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var storedProcedure = "spCreateUserContact";
                var keyValue = new Dictionary<string, object>()
                {
                    {"@ClientId", clientId },
                    {"@PhoneNumber", phoneNumber }
                };
                var result = DatabaseUtils.ExecuteSP(storedProcedure, keyValue, connectionString, connection);

                connection.Close();
            }
        }

        private int GetClientId(CRMUserInfo userInfo, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var storedProcedure = "spReadUserByLogin";
                var keyValue = new Dictionary<string, object>()
                {
                    {"@Login", userInfo.Login },
                    {"@LineNumber", userInfo.LineNumber }
                };
                var result = DatabaseUtils.ExecuteSP(storedProcedure, keyValue, connectionString, connection);
                int clientId = (int)result.FirstOrDefault().FirstOrDefault().GetValueOrDefault("Id");
                connection.Close();
                return clientId;
            }
        }
    }
}