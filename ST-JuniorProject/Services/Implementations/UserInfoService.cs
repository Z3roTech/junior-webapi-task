using Microsoft.Data.SqlClient;
using ST_JuniorProject.Models;
using ST_JuniorProject.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using StranzitOnline.Common.Tools;
using Microsoft.Extensions.Configuration;

namespace ST_JuniorProject.Services.Implementations
{
    /// <summary>
    /// Класс работы с пользовательской информацией в БД
    /// </summary>
    public class UserInfoService : IUserInfoService
    {
        private IConfiguration Configuration;

        public UserInfoService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Обновление пользовательских контактов в БД
        /// </summary>
        /// <param name="userInfo">Информация о пользователе в БД</param>
        /// <param name="phoneNumber">Новый контакт пользователя</param>
        public void UpdateUserContact(CRMUserInfo userInfo, string phoneNumber)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int clientId = GetClientId(userInfo, connectionString);
            CreateNewUserContact(phoneNumber, connectionString, clientId);
        }

        /// <summary>
        /// Создание нового контакта пользователя в БД
        /// </summary>
        /// <param name="phoneNumber">Новый номер телефона пользователя</param>
        /// <param name="connectionString"></param>
        /// <param name="clientId">Идентификатор пользователя в БД</param>
        private void CreateNewUserContact(string phoneNumber, string connectionString, int clientId)
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

        /// <summary>
        /// Получение идентификатора пользователя в БД
        /// </summary>
        /// <param name="userInfo">Данные пользователя в БД: Логин и номер</param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
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