using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ST_JuniorProject.Models;
using ST_JuniorProject.Services.Interfaces;
using StranzitOnline.Common.Tools;
using System.Collections.Generic;
using System.Linq;

namespace ST_JuniorProject.Services.Implementations
{
    /// <summary>
    /// Класс работы с информацией пользователя CRM-системы
    /// </summary>
    public class CrmUserInfoService : ICrmUserInfoService
    {
        private IConfiguration Configuration;

        public CrmUserInfoService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Получение информации о пользователе CRM-системы для обновления данных в БД
        /// </summary>
        /// <param name="userRequest">Уведомление CRM-системы</param>
        /// <returns>Параметры пользователя в БД</returns>
        public CRMUserInfo GetCRMUserInfo(CRMUserRequest userRequest)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var storedProcedure = "spReadCRMUserInfoByCuratorId";
                var keyValue = new Dictionary<string, object>()
                {
                    {"@CuratorId", userRequest.CuratorId }
                };
                var result = DatabaseUtils.ExecuteSP(storedProcedure, keyValue, connectionString, connection);
                connection.Close();
                return new CRMUserInfo()
                {
                    Id = (int)result.FirstOrDefault().FirstOrDefault().GetValueOrDefault("Id"),
                    CuratorId = (int)result.FirstOrDefault().FirstOrDefault().GetValueOrDefault("CuratorId"),
                    Login = (int)result.FirstOrDefault().FirstOrDefault().GetValueOrDefault("Login"),
                    LineNumber = (int)result.FirstOrDefault().FirstOrDefault().GetValueOrDefault("LineNumber"),
                };
            }
        }
    }
}